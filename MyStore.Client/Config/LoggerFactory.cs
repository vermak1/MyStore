using System;

namespace MyStore.Client
{
    internal static class LoggerFactory
    {
        public static ILogger GetDefaultLogger()
        {
            return new FileLogger();
        }
    }
}
