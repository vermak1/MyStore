using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class SocketClientContextHolder : IClientContextHolder
    {
        private readonly Socket _clientSocket;

        private readonly ILogger _logger;

        public IMessenger Messenger { get; }
        public SocketClientContextHolder(Socket clientSocket, ILogger logger)
        {
            try
            {
                _logger = logger;
                _clientSocket = clientSocket;
                Messenger = new SocketMessenger(_clientSocket, logger);
            }
            catch(Exception ex)
            {
                _clientSocket?.Dispose();
                _logger.Exception(ex, String.Format("Failed to create client context"));
            }
            
        }
        
        public void Dispose()
        {
            _clientSocket?.Dispose();
        }

    }
}
