using System.Net;

namespace MyStore.Server
{
    internal class ConnectionProvider : IConnectionConfigurator
    {
        private readonly IServerSocketSettingsProvider _settings;
        public ConnectionProvider()
        {
            _settings = new SocketSettingsHardcodeProvider();
        }

        public IClientAwaiter GetClientAwaiter()
        {
            return GetDefaultClientAwaiter();
        }

        private IClientAwaiter GetDefaultClientAwaiter()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, _settings.Port);
            return new TcpListenerClientAwaiter(endPoint);
        }
    }
}
