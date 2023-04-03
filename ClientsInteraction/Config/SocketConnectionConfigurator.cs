using System;
using System.Net.Sockets;

namespace MyStore.Server
{
    internal class SocketConnectionConfigurator : IConnectionConfigurator
    {

        private readonly IServerSocketProvider _socketProvider;

        private readonly IServerSocketSettingsProvider _settingsProvider;

        private readonly ILogger _logger;

        private readonly Socket _socket;

        public SocketConnectionConfigurator(ILogger logger)
        {
            _settingsProvider = new SocketSettingsHardcodeProvider();
            _socketProvider = new SocketProvider(_settingsProvider);
            _logger = logger;
            try
            {
                _socket = _socketProvider.ConfigureServerSocket();
            }
            catch(Exception ex)
            {
                _logger.Exception(ex, "Failed to configure Socket");
                _socket?.Dispose();
                throw;
            }
        }

        public IServerConnectionHolder GetConnectionHolder()
        {
            return new ServerSocketConnectionHolder(_logger, _socket, _settingsProvider);
        }

        public IClientAwaiter GetClientAwaiter()
        {
            return new SocketClientAwaiter(_socket, _logger);
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }
    }
}
