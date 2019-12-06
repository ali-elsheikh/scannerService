
using System;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace namePipeTransfere
{
    class servicePipe
    {
        static string PipeName = "QMM_Scanner";
        const int BufferSize = 4096;
        static NamedPipeClientStream pipeStream = new NamedPipeClientStream(".",
                    PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
        static byte[] bytes = new byte[BufferSize];


        public static byte[] GetMessageFromServer()
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
            return bytes;
        }
    }

    public class scannerPipe
    {

        static NamedPipeServerStream pipeStream;
        static scannerPipe()
        {
            PipeSecurity ps = new PipeSecurity();
            PipeAccessRule psRule = new PipeAccessRule(@"Everyone", PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow);
            ps.AddAccessRule(psRule);
            pipeStream = new NamedPipeServerStream("QMM_Scanner", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous, 1, 1, ps);

        }
        public static void connect()
        {
            if (pipeStream.IsConnected == false)
            {
                pipeStream.WaitForConnection();
            }
        }
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

        public static byte[] GetScannedDocument()
        {
            pipeStream.ReadMode = PipeTransmissionMode.Message;
            const int BufferSize = 4069;
            byte[] bytes;
            List<byte[]> temp = new List<byte[]>();

            do
            {
                bytes = new byte[BufferSize];
                pipeStream.Read(bytes, 0, BufferSize);
                temp.Add(bytes);
                bytes = null;
            } while (!pipeStream.IsMessageComplete);

            byte[] buff = new byte[temp.Count * 4069];
            int k = 0;

            foreach (byte[] arr in temp)
            {
                for (int i = 0, length= arr.Length; i < length; i++)
                {
                    buff[k++] = arr[i];
                }
            }

            pipeStream.Disconnect();
            return buff;
        }

        public static List<byte[]> deserializeScannedList()
        {
            StreamReader reader = new StreamReader(pipeStream);
            BinaryFormatter formatterDeserialize = new BinaryFormatter();
            List<byte[]> retrievedList = (List<byte[]>)formatterDeserialize.Deserialize(reader.BaseStream);
            pipeStream.Disconnect();
            return retrievedList;
        }
    }
}