using System;

namespace MyStore.Client
{
    internal class LoggerConfigurator
    {
        public ILogger GetDefaultLogger()
        {
            return new FileLogger();
        }
    }
}
