using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class TcpClientMessenger : IMessenger
    {
        private const Int32 BUFFER_SIZE = 512;

        private TcpClient _tcpClient;

        private readonly IRetryOptionsProvider _retryOptions;

        private readonly ILogger _logger;

        private readonly ISocketSettingsProvider _settingsProvider;

        private Boolean IsConnectedNow
        {
            get
            {
                try
                {
                    if (_tcpClient != null && _tcpClient.Client != null && _tcpClient.Client.Connected)
                    {
                        
                        if (_tcpClient.Client.Poll(0, SelectMode.SelectRead))
                        {
                            byte[] buff = new byte[1];
                            if (_tcpClient.Client.Receive(buff, SocketFlags.Peek) == 0)
                                return false;
                            
                            return true;
                        }

                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        private Boolean WasConnected;
        public TcpClientMessenger(IRetryOptionsProvider retryOptions, ISocketSettingsProvider settings)
        {
            try
            {
                _settingsProvider = settings;
                _retryOptions = retryOptions;
                _logger = Configurator.Instance.GetLogger();
                _tcpClient = new TcpClient();
            }
            catch (Exception ex)
            {
                _tcpClient?.Dispose();
                _logger.Exception(ex, "Failed to initialize TcpClient messenger");
                throw;
            }
        }
        public void Dispose()
        {
            _tcpClient?.Dispose();
        }

        public async Task<Boolean> ConnectToServerAsync()
        {
            if (IsConnectedNow && WasConnected)
                return false;

            if (WasConnected && !IsConnectedNow)
                RecreateClient();

            _logger.Info("Trying to connect to server {0}:{1}", _settingsProvider.IP, _settingsProvider.Port);
            for (int i = 0; i < _retryOptions.RetryCount; i++)
            {
                try
                {
                    await _tcpClient.ConnectAsync(_settingsProvider.IP, _settingsProvider.Port);
                    if (_tcpClient.Connected)
                    {
                        _logger.Info("Successully connected to server {0}:{1}", _settingsProvider.IP, _settingsProvider.Port);
                        WasConnected = true;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Exception(ex, "Failed to connect to server[{0}:{1}]. Retry {2} of {3}", _settingsProvider.IP, _settingsProvider.Port, i + 1, _retryOptions.RetryCount);
                    Thread.Sleep(_retryOptions.RetryInterval);
                }
            }
            return true;
        }

        private void RecreateClient()
        {
            DisconnectFromServer();
            _logger.Info("Trying to recreate TcpClient and reconnect...");
            _tcpClient = new TcpClient();
        }

        public void DisconnectFromServer()
        {
            _logger.Info("Disconnecting from server {0}:{1}", _settingsProvider.IP, _settingsProvider.Port);
            _tcpClient.Close();
        }

        public async Task<string> SendAndReceiveMessageAsync(string message)
        {
            await SendMessageInternalAsync(message);
            return await ReceiveMessageInternalAsync();
        }

        private async Task SendMessageInternalAsync(String message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentException(nameof(message));

            try
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await _tcpClient.GetStream().WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception ex) 
            {
                _logger.Exception(ex, "Sending message failed to: {0}", _tcpClient.Client.RemoteEndPoint);
                throw;
            }
        }

        private async Task<String> ReceiveMessageInternalAsync()
        {
            StringBuilder sb = new StringBuilder();
            byte[] buffer = new byte[BUFFER_SIZE];
            try
            {
                do
                {
                    Int32 bytes = await _tcpClient.GetStream().ReadAsync(buffer, 0, BUFFER_SIZE);
                    sb.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
                }
                while (_tcpClient.GetStream().DataAvailable);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, "Receiving message failed from: {0}", _tcpClient.Client.RemoteEndPoint);
                throw;
            }
            return sb.ToString();
        }
    }
}
