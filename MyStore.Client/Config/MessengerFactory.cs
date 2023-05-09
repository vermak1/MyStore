using System;

namespace MyStore.Client
{
    internal static class MessengerFactory
    {
        public static IMessenger GetTcpClientMessenger()
        {
            RetryOptionsHardcodeProvider retries = new RetryOptionsHardcodeProvider();
            SocketSettingsHardcodeProvider settings = new SocketSettingsHardcodeProvider();
            return new TcpClientMessenger(retries, settings);
        }
    }
}
