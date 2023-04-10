using System;

namespace MyStore.Client
{
    internal class MessengerConfiguration
    {
        public IMessenger GetDefaultMessenger(ILogger logger)
        {
            SocketSettingsHardcodeProvider settings = new SocketSettingsHardcodeProvider();
            SocketProvider socketProvider = new SocketProvider(settings);
            RetryOptionsHardcodeProvider retries = new RetryOptionsHardcodeProvider();
            return new SocketMessenger(logger, socketProvider, settings, retries);
        }
    }
}
