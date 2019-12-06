using System;

namespace twainNative
{
    public class ScanningCompleteEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public ScanningCompleteEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }
}
