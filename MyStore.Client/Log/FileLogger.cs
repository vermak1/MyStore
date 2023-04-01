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

        public void Error(String message, params object[] args)
        {
            LogInternalWithArgs(message, ELogSeverity.Error, args);
        }

        public void Exception(Exception ex, string message)
        {
            LogInternal(message, ELogSeverity.Error);
            LogInternal(ex.StackTrace, ELogSeverity.Error);
        }

        public void Exception(Exception ex, string message, params object[] args)
        {
            LogInternalWithArgs(message, ELogSeverity.Error, args);
            LogInternal(ex.StackTrace, ELogSeverity.Error);
        }

        public void Info(string message)
        {
            LogInternal(message, ELogSeverity.Info);
        }

        public void Info(String message, params object[] args)
        {
            LogInternalWithArgs(message, ELogSeverity.Info, args);
        }

        public void Warning(string message)
        {
            LogInternal(message, ELogSeverity.Warning);
        }

        public void Warning(String message, params object[] args)
        {
            LogInternalWithArgs(message, ELogSeverity.Warning, args);
        }

        private void LogInternalWithArgs(String message, ELogSeverity severity, params object[] args)
        {
            if (args == null)
            {
                LogInternal(message, severity);
                return;
            }

            String formatted = String.Format(message, args);
            LogInternal(formatted, severity);
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
