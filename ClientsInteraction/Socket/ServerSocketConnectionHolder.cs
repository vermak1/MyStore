using System;
using System.Net.Sockets;
using System.Net;

namespace MyStore.Server
{
    internal class ServerSocketConnectionHolder : IServerConnectionHolder
    {
        public Socket ServerSocket { get; }

        private readonly IServerSocketProvider _socketProvider;

        private readonly IServerSocketSettingsProvider _settingsProvider;

        private readonly ILogger _logger;
        public ServerSocketConnectionHolder(ILogger logger)
        {
            _socketProvider = new SocketProvider();
            _settingsProvider = new SocketSettingsHardcodeProvider();
            ServerSocket = _socketProvider.ConfigureServerSocket();
            _logger = logger;
        }

        private void BindAndStartListen()
        {
            IPAddress ipAddr = IPAddress.Parse(_settingsProvider.IP);
            IPEndPoint endpoint = new IPEndPoint(ipAddr, _settingsProvider.Port);
            ServerSocket.Bind(endpoint);
            ServerSocket.Listen(_settingsProvider.MaxConnections);
            _logger.Info("Socket started to bind and listen");
        }

        public void OpenConnection()
        {
            BindAndStartListen();
        }

        public void CloseConnection()
        {
            ServerSocket?.Close();
        }

        public void Dispose()
        {
            ServerSocket?.Dispose();
        }
    }
}