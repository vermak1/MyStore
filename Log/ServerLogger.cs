using System;
using System.IO;
using System.Threading;

namespace MyStore.Server
{
    internal class ServerLogger : ILogger
    {
        private readonly String _path;
        public ServerLogger(String path)
        {
            _path = path;
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

            String formatted = String.Format("[{0}] <{1}>\t[{2}]\t{3}\n", DateTime.Now, Thread.CurrentThread.ManagedThreadId, severity, message);
            File.AppendAllText(_path, formatted);
        }
    }
}
