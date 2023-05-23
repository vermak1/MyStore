using System;
using System.IO;
using System.Threading;

namespace MyStore.Server
{
    internal static class Log
    {
        private static readonly String s_path = "MyStore.Server.log";

        private static readonly Object s_sync = new Object();
        public static void Error(string message)
        {
            LogInternal(message, ELogSeverity.Error);
        }

        public static void Error(String message, params object[] args)
        {
            LogInternalWithArgs(message, ELogSeverity.Error, args);
        }

        public static void Exception(Exception ex, string message)
        {
            LogInternal(message, ELogSeverity.Error);
            ExceptionInternal(ex);
        }

        public static void Exception(Exception ex)
        {
            ExceptionInternal(ex);
        }

        public static void Exception(Exception ex, string message, params object[] args)
        {
            LogInternalWithArgs(message, ELogSeverity.Error, args);
            ExceptionInternal(ex);
        }

        public static void Info(string message)
        {
            LogInternal(message, ELogSeverity.Info);
        }

        public static void Info(String message, params object[] args)
        {
            LogInternalWithArgs(message, ELogSeverity.Info, args);
        }

        public static void Warning(string message)
        {
            LogInternal(message, ELogSeverity.Warning);
        }

        public static void Warning(String message, params object[] args)
        {
            LogInternalWithArgs(message, ELogSeverity.Warning, args);
        }

        private static void ExceptionInternal(Exception ex)
        {
            LogInternal(ex.Message, ELogSeverity.Error);
            LogInternal(ex.StackTrace, ELogSeverity.Error);
        }

        private static void LogInternalWithArgs(String message, ELogSeverity severity, params object[] args)
        {
            if (args == null)
            {
                LogInternal(message, severity);
                return;
            }

            String formatted = String.Format(message, args);
            LogInternal(formatted, severity);
        }

        private static void LogInternal(String message, ELogSeverity severity)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentException(message, nameof(message));
            foreach(var line in message.Split('\n')) 
            {
                String formatted = String.Format("[{0}] <{1}>\t[{2}]\t{3}\n", DateTime.Now, Thread.CurrentThread.ManagedThreadId, severity, line);
                lock (s_sync)
                    File.AppendAllText(s_path, formatted);
            }
        }
    }
}
