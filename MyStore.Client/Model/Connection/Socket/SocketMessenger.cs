using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class SocketMessenger : IMessenger
    {
        private const Int32 BUFFER_SIZE = 512;

        private readonly ILogger _logger;

        private readonly ISocketSettingsProvider _settingsProvider;

        private readonly IRetryOptionsProvider _retryOptions;

        private readonly Socket _socket;

        public SocketMessenger(ISocketProvider socketProvider, ISocketSettingsProvider settings, IRetryOptionsProvider retryOptions)
        {
            _logger = Configurator.Instance.GetLogger();
            _retryOptions = retryOptions;
            _settingsProvider = settings;
            try
            {
                _socket = socketProvider.ConfigureServerSocket();
            }
            catch (Exception ex) 
            {
                _socket?.Dispose();
                _logger.Exception(ex, "Failed to initialize Socket messenger");
                throw;
            }
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }

        public async Task<string> SendAndReceiveMessageAsync(String message)
        {
            await SendMessageInternalAsync(message);
            return await ReceiveMessageInternalAsync();
        }

        public async Task<bool> ConnectToServerAsync()
        {
            _logger.Info("Trying to connect to server {0}:{1}", _settingsProvider.IP, _settingsProvider.Port);
            for (int i = 0; i < _retryOptions.RetryCount; i++)
            {
                try
                {
                    await _socket.ConnectAsync(_settingsProvider.IP, _settingsProvider.Port);
                    if (_socket.Connected)
                    {
                        _logger.Info("Successully connected to server {0}:{1}", _settingsProvider.IP, _settingsProvider.Port);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Exception(ex, "Failed to connect to server[{0}:{1}]. Retry {2} of {3}", _settingsProvider.IP, _settingsProvider.Port, i + 1, _retryOptions.RetryCount);
                    Thread.Sleep(_retryOptions.RetryInterval);
                }
            }
            return false;
        }

        public void DisconnectFromServer()
        {
            _logger.Info("Disconnecting from server {0}:{1}", _settingsProvider.IP, _settingsProvider.Port);
            _socket?.Disconnect(true);
        }

        private async Task<Boolean> ReconnectAsync()
        {
            DisconnectFromServer();
            return await ConnectToServerAsync();
        }

        private async Task SendMessageInternalAsync(String message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentException(nameof(message));

            try
            {
                ArraySegment<Byte> buffer = new ArraySegment<Byte>(Encoding.UTF8.GetBytes(message));
                await _socket.SendAsync(buffer, SocketFlags.None);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, "Sending message failed to: {0}", _socket.RemoteEndPoint);
                throw;
                //if (await ReconnectAsync())
                    //await SendMessageInternalAsync(message);
            }
        }

        private async Task<String> ReceiveMessageInternalAsync()
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new Byte[BUFFER_SIZE]);
            StringBuilder sb = new StringBuilder();
            try
            {
                do
                {
                    Int32 bytes = await _socket.ReceiveAsync(buffer, 0);
                    sb.Append(Encoding.UTF8.GetString(buffer.Array, 0, bytes));
                }
                while (_socket.Available != 0);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, "Receiving message failed from: {0}", _socket.RemoteEndPoint);
                throw;
                //if (await ReconnectAsync())
                    //await ReceiveMessageInternalAsync();
            }
            return sb.ToString();
        }
    }
}
