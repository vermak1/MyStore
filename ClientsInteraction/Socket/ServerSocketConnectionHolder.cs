using System;
using System.Net.Sockets;
using System.Net;

namespace MyStore.Server
{
    internal class ServerSocketConnectionHolder : IServerConnectionHolder
    {
        private readonly Socket _serverSocket;

        private readonly IServerSocketSettingsProvider _settingsProvider;

        private readonly ILogger _logger;
        public ServerSocketConnectionHolder(ILogger logger, Socket socket, IServerSocketSettingsProvider settings)
        {
            _logger = logger;
            _settingsProvider = settings;
            try
            {
                _serverSocket = socket;
            }
            catch (Exception ex)
            {
                _serverSocket?.Dispose();
                _logger.Exception(ex, "Failed to initialize server socket connection");
                throw;
            }
        }

        private void BindAndStartListen()
        {
            IPAddress ipAddr = IPAddress.Parse(_settingsProvider.IP);
            IPEndPoint endpoint = new IPEndPoint(ipAddr, _settingsProvider.Port);
            _serverSocket.Bind(endpoint);
            _serverSocket.Listen(_settingsProvider.MaxConnections);
            _logger.Info("Socket started to bind and listen");
        }

        public void OpenConnection()
        {
            BindAndStartListen();
        }

        public void CloseConnection()
        {
            _serverSocket?.Close();
        }

        public void Dispose()
        {
            _serverSocket?.Dispose();
        }
    }
}