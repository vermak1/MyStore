using System.Net.Sockets;


namespace MyStore.Client
{
    internal class SocketProvider : ISocketProvider
    {
        private readonly ISocketSettingsProvider _settingsProvider;
        public SocketProvider(ISocketSettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }
        public Socket ConfigureServerSocket()
        {
            return new Socket(_settingsProvider.AddressFamily, _settingsProvider.SocketType, _settingsProvider.ProtocolType);
        }
    }
}
