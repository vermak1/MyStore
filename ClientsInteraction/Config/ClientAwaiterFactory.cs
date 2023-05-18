using System.Net;

namespace MyStore.Server
{
    internal class ClientAwaiterFactory
    {
        private readonly static IServerSocketSettingsProvider _settings = new SocketSettingsHardcodeProvider();
        
        public static IClientAwaiter GetClientAwaiter()
        {
            return GetDefaultClientAwaiter();
        }

        private static IClientAwaiter GetDefaultClientAwaiter()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, _settings.Port);
            return new TcpListenerClientAwaiter(endPoint);
        }
    }
}
