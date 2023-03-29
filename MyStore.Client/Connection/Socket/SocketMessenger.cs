using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class SocketMessenger : IMessenger
    {
        private const Int32 BUFFER_SIZE = 512;

        private readonly SocketConnection _conenction;

        private readonly ILogger _logger;
        public SocketMessenger(SocketConnection socketConnection, ILogger logger)
        {
            _conenction = socketConnection;
            _logger = logger;
        }
        public async Task<string> ReceiveMessageAsync()
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new Byte[BUFFER_SIZE]);
            StringBuilder sb = new StringBuilder();
            try
            {
                do
                {
                    Int32 bytes = await _conenction.ServerSocket.ReceiveAsync(buffer, 0);
                    sb.Append(Encoding.UTF8.GetString(buffer.Array, 0, bytes));
                }
                while (_conenction.ServerSocket.Available != 0);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, String.Format("Receiving message failed from: {0}", _conenction.ServerSocket.RemoteEndPoint));
                throw;
            }
        }

        public async Task SendMessageAsync(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentException(nameof(message));

            try
            {
                ArraySegment<Byte> buffer = new ArraySegment<Byte>(Encoding.UTF8.GetBytes(message));
                await _conenction.ServerSocket.SendAsync(buffer, SocketFlags.None);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, String.Format("Sending message failed to: {0}", _conenction.ServerSocket.RemoteEndPoint));
                throw;
            }
        }
    }
}
