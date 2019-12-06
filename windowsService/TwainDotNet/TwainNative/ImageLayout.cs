using System.Runtime.InteropServices;

namespace twainNative.TwainNative
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class ImageLayout
    {
        public Frame Frame;

        public uint DocumentNumber;

        public uint PageNumber;

        public uint FrameNumber;        
    }
}