using System.Net.Sockets;


namespace MyStore.Server
{
    internal class SocketProvider : IServerSocketProvider
    {
        private readonly IServerSocketSettingsProvider _settingsProvider;

        public SocketProvider(IServerSocketSettingsProvider settings)
        {
            _settingsProvider = settings;
        }

        public Socket ConfigureServerSocket()
        {
            return new Socket(_settingsProvider.AddressFamily, _settingsProvider.SocketType, _settingsProvider.ProtocolType);
        }
    }
}
