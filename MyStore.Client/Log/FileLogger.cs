using System;
using System.Diagnostics;
using System.Threading;

namespace MyStore.Client
{
    internal class FileLogger : ILogger
    {
        private readonly TraceSource _traceSource;
        public FileLogger()
        {
            _traceSource = new TraceSource("ClientApp");
        }

        public void Error(string message)
        {
            LogInternal(message, ELogSeverity.Error);
        }

        public void Exception(Exception ex, string message)
        {
            LogInternal(message, ELogSeverity.Error);
            LogInternal(ex.StackTrace, ELogSeverity.Error);
        }

        public void Info(string message)
        {
            LogInternal(message, ELogSeverity.Info);
        }

        public void Warning(string message)
        {
            LogInternal(message, ELogSeverity.Warning);
        }

        private void LogInternal(String message, ELogSeverity severity)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentException(message, nameof(message));

            String formatted = String.Format("[{0}] <{1}>\t[{2}]\t{3}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, severity, message);
            _traceSource.TraceInformation(formatted);
            _traceSource.Flush();
        }
    }
}
