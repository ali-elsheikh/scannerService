using System;
using System.Drawing;

namespace twainNative
{
    public class TransferImageEventArgs : EventArgs
    {
        public Bitmap Image { get; private set; }
        public bool ContinueScanning { get; set; }

        public TransferImageEventArgs(Bitmap image, bool continueScanning)
        {
            this.Image = image;
            this.ContinueScanning = continueScanning;
        }
    }
}
