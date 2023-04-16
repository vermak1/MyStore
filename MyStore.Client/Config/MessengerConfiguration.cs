using System;

namespace MyStore.Client
{
    internal class MessengerConfiguration
    {
        public IMessenger GetDefaultMessenger()
        {
            SocketSettingsHardcodeProvider settings = new SocketSettingsHardcodeProvider();
            SocketProvider socketProvider = new SocketProvider(settings);
            RetryOptionsHardcodeProvider retries = new RetryOptionsHardcodeProvider();
            return new SocketMessenger(socketProvider, settings, retries);
        }

        public IMessenger GetTcpClientMessenger()
        {
            RetryOptionsHardcodeProvider retries = new RetryOptionsHardcodeProvider();
            SocketSettingsHardcodeProvider settings = new SocketSettingsHardcodeProvider();
            return new TcpClientMessenger(retries, settings);
        }
    }
}
