using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class TcpMessenger : IMessenger
    {
        private const Int32 BUFFER_SIZE = 512;

        private readonly TcpClient _tcpClient;
        public TcpMessenger(TcpClient client)
        {
            _tcpClient = client;
        }
        public async Task<String> ReceiveMessageAsync()
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
                Log.Exception(ex, "Receiving message failed from: {0}", _tcpClient.Client.RemoteEndPoint);
                throw;
            }
            return sb.ToString();
        }

        public async Task SendMessageAsync(string message)
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
                Log.Exception(ex, "Sending message failed to: {0}", _tcpClient.Client.RemoteEndPoint);
                throw;
            }
        }
    }
}
