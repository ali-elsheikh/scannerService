using System;
using System.Text;
using System.IO.Pipes;


using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace namedPipeTransfere
{
    public class scannerPipe
    {
        static bool readSetUp = false;
        static string PipeName = "QMM_Scanner";
        static NamedPipeServerStream pipeStream = new NamedPipeServerStream(PipeName, PipeDirection.InOut,
                      1, PipeTransmissionMode.Message, PipeOptions.WriteThrough);


        public static void SendFilesToService(Byte[] bytes)
        {
            if (pipeStream.IsConnected == false)
            {
                pipeStream.WaitForConnection();
            }
            // sending messages
            //UTF8Encoding encoding = new UTF8Encoding();
            // byte[] bytes = encoding.GetBytes(msgInp);
            pipeStream.Write(bytes, 0, bytes.Length);
        }


    }

    class servicePipe
    {
        static string PipeName = "QMM_Scanner";
        const int BufferSize = 16384;
        static NamedPipeClientStream pipeStream = new NamedPipeClientStream(".",
                    PipeName, PipeDirection.Out, PipeOptions.Asynchronous, System.Security.Principal.TokenImpersonationLevel.Impersonation);
        static byte[] bytes = new byte[BufferSize];


        public static void GetMessageFromServer()
        {

            byte[] temp = new byte[BufferSize];
            if (pipeStream.IsConnected == false)
            {
                pipeStream.Connect();
            }
            pipeStream.ReadMode = PipeTransmissionMode.Message;
            Decoder decoder = Encoding.UTF8.GetDecoder();
            char[] chars = new char[BufferSize];
            int numBytes = 0;
            StringBuilder msg = new StringBuilder();
            do
            {
                msg.Length = 0;
                do
                {
                    numBytes = pipeStream.Read(bytes, 0, BufferSize);
                    if (numBytes > 0)
                    {
                        int numChars = decoder.GetCharCount(bytes, 0, numBytes);
                        decoder.GetChars(bytes, 0, numBytes, chars, 0, false);
                        msg.Append(chars, 0, numChars);
                    }
                } while (numBytes > 0 && !pipeStream.IsMessageComplete);
                decoder.Reset();

            } while (numBytes != 0);
        }

        public static void SendMessageToServer(byte[] buff)
        {
            if (pipeStream.IsConnected == false)
            {
                pipeStream.Connect();
            }
            pipeStream.Write(buff, 0, buff.Length);
            pipeStream.Close();
        }

        public static void serveScanRes(System.Collections.Generic.List<byte[]> bytes)
        {
            if (pipeStream.IsConnected == false)
            {
                pipeStream.Connect();
            }
            System.IO.StreamWriter writer = new System.IO.StreamWriter(pipeStream);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(writer.BaseStream, bytes);
            writer.Flush();
            //pipeStream.Close();
        }
    }

}
