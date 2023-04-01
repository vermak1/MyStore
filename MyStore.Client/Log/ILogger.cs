using System;

namespace MyStore.Client
{
    internal interface ILogger
    {
        void Info(String message);

        void Info(String message, params object[] args);

        void Warning(String message);

        void Warning(String message, params object[] args);

        void Error(String message);

        void Error(String message, params object[] args);

        void Exception(Exception ex, String message);

        void Exception(Exception ex, String message, params object[] args);
    }
}
