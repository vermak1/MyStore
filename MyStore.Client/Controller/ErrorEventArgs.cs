using System;

namespace MyStore.Client
{
    internal class ErrorEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public String Message { get; }

        public ErrorEventArgs(Exception exception, string message)
        {
            Exception = exception;
            Message = message;
        }
    }
}
