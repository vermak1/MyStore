using System;

namespace MyStore.Client
{
    internal interface ILogger
    {
        void Info(String message);

        void Warning(String message);

        void Error(String message);

        void Exception(Exception ex, String message);
    }
}
