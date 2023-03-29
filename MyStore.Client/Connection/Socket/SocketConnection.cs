using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace MyStore.Client
{
    internal class SocketConnection : IConnectionHolder, IDisposable
    {
        private readonly ISocketProvider _socketProvider;

        private readonly ISocketSettingsProvider _settingsProvider;

        private readonly ILogger _logger;

        private readonly IRetryOptionsProvider _retryOptions;

        public Socket ServerSocket { get; }

        public SocketConnection(ISocketProvider socketProvider, ISocketSettingsProvider settingsProvider, ILogger logger, IRetryOptionsProvider retryOptions)
        {
            _socketProvider = socketProvider;
            _settingsProvider = settingsProvider;
            _retryOptions = retryOptions;
            _logger = logger;
            try
            {
                ServerSocket = _socketProvider.ConfigureServerSocket();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, String.Format("Failed to configure Socket"));
                throw;
            }
        }

        public async Task<Boolean> ConnectToServerAsync()
        {
            for (int i = 0; i < _retryOptions.RetryCount; i++)
            {
                try
                {
                    await ServerSocket.ConnectAsync(_settingsProvider.IP, _settingsProvider.Port);
                    if (ServerSocket.Connected)
                    {
                        _logger.Info(String.Format("Successully connected to server {0}:{1}", _settingsProvider.IP, _settingsProvider.Port));
                        return true;
                    }
                    
                }
                catch (Exception ex)
                {
                    _logger.Exception(ex, String.Format("Failed to connect to server[{0}:{1}]. Retry {2} of {3}", _settingsProvider.IP, _settingsProvider.Port, i + 1, _retryOptions.RetryCount));
                    Thread.Sleep(_retryOptions.RetryInterval);
                }
            }
            return false;
            
        }

        public void DisconnectFromServer()
        {
            ServerSocket.Disconnect(true);
        }

        public void Dispose()
        {
            ServerSocket?.Dispose();
        }
    }
}
