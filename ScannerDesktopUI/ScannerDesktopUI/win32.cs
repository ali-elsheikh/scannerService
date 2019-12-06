﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using log4net.Core;
using log4net;

namespace twainNative
{
    public static class Kernel32Native
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalAlloc(GlobalAllocFlags flags, int size);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern bool GlobalUnlock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);
    }

    [Flags]
    public enum GlobalAllocFlags : uint
    {
        MemFixed = 0,

        MemMoveable = 0x2,

        ZeroInit = 0x40,

        Handle = MemMoveable | ZeroInit
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public class BitmapInfoHeader
    {
        public int Size;
        public int Width;
        public int Height;
        public short Planes;
        public short BitCount;
        public int Compression;
        public int SizeImage;
        public int XPelsPerMeter;
        public int YPelsPerMeter;
        public int ClrUsed;
        public int ClrImportant;

        public override string ToString()
        {
            return string.Format(
                "s:{0} w:{1} h:{2} p:{3} bc:{4} c:{5} si:{6} xpels:{7} ypels:{8} cu:{9} ci:{10}",
                Size,
                Width,
                Height,
                Planes,
                BitCount,
                Compression,
                SizeImage,
                XPelsPerMeter,
                YPelsPerMeter,
                ClrUsed,
                ClrImportant);
        }
    }

    public static class Gdi32Native
    {
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern int SetDIBitsToDevice(IntPtr hdc, int xdst, int ydst, int width, int height,
            int xsrc, int ysrc, int start, int lines, IntPtr bitsptr, IntPtr bmiptr, int color);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern bool DeleteObject(IntPtr hObject);
    }
    public class BitmapRenderer : IDisposable
    {
        /// <summary>
        /// The logger for this class.
        /// </summary>
        static ILog log = LogManager.GetLogger(typeof(BitmapRenderer));

        IntPtr _dibHandle;
        IntPtr _bitmapPointer;
        IntPtr _pixelInfoPointer;
        Rectangle _rectangle;
        BitmapInfoHeader _bitmapInfo;

        public BitmapRenderer(IntPtr dibHandle)
        {
            _dibHandle = dibHandle;
            _bitmapPointer = Kernel32Native.GlobalLock(dibHandle);

            _bitmapInfo = new BitmapInfoHeader();
            Marshal.PtrToStructure(_bitmapPointer, _bitmapInfo);
            log.Debug(_bitmapInfo.ToString());

            _rectangle = new Rectangle();
            _rectangle.X = _rectangle.Y = 0;
            _rectangle.Width = _bitmapInfo.Width;
            _rectangle.Height = _bitmapInfo.Height;

            if (_bitmapInfo.SizeImage == 0)
            {
                _bitmapInfo.SizeImage = ((((_bitmapInfo.Width * _bitmapInfo.BitCount) + 31) & ~31) >> 3) * _bitmapInfo.Height;
            }


            // The following code only works on x86
            Debug.Assert(Marshal.SizeOf(typeof(IntPtr)) == 4);

            int pixelInfoPointer = _bitmapInfo.ClrUsed;
            if ((pixelInfoPointer == 0) && (_bitmapInfo.BitCount <= 8))
            {
                pixelInfoPointer = 1 << _bitmapInfo.BitCount;
            }

            pixelInfoPointer = (pixelInfoPointer * 4) + _bitmapInfo.Size + _bitmapPointer.ToInt32();

            _pixelInfoPointer = new IntPtr(pixelInfoPointer);
        }

        ~BitmapRenderer()
        {
            Dispose(false);
        }

        public Bitmap RenderToBitmap()
        {
            Bitmap bitmap = new Bitmap(_rectangle.Width, _rectangle.Height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                IntPtr hdc = graphics.GetHdc();

                try
                {
                    Gdi32Native.SetDIBitsToDevice(hdc, 0, 0, _rectangle.Width, _rectangle.Height,
                        0, 0, 0, _rectangle.Height, _pixelInfoPointer, _bitmapPointer, 0);
                }
                finally
                {
                    graphics.ReleaseHdc(hdc);
                }
            }

            bitmap.SetResolution(PpmToDpi(_bitmapInfo.XPelsPerMeter), PpmToDpi(_bitmapInfo.YPelsPerMeter));

            return bitmap;
        }

        private static float PpmToDpi(double pixelsPerMeter)
        {
            double pixelsPerMillimeter = (double)pixelsPerMeter / 1000.0;
            double dotsPerInch = pixelsPerMillimeter * 25.4;
            return (float)Math.Round(dotsPerInch, 2);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Kernel32Native.GlobalUnlock(_dibHandle);
            Kernel32Native.GlobalFree(_dibHandle);
        }
    }
    public static class User32Native
    {
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int GetMessagePos();

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int GetMessageTime();
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct WindowsMessage
    {
        public IntPtr hwnd;
        public int message;
        public IntPtr wParam;
        public IntPtr lParam;
        public int time;
        public int x;
        public int y;
    }
}



