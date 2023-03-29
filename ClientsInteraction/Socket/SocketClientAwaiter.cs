using System;
using System.Net.Sockets;

namespace MyStore.Server
{
    internal class SocketClientAwaiter : IClientAwaiter
    {
        private readonly Socket _serverSocket;

        private readonly ILogger _logger;

        public SocketClientAwaiter(Socket serverSocket, ILogger logger)
        {
            _serverSocket = serverSocket;
            _logger = logger;
        }

        public IClientContextHolder WaitingForClient()
        {
            _logger.Info("Waiting for new client");
            Socket clientSocket = null;
            try
            {
                clientSocket = _serverSocket.Accept();
                _logger.Info(String.Format("Client with IP {0} has been connected", clientSocket.RemoteEndPoint));
            }
            catch (Exception ex) 
            {
                clientSocket?.Dispose();
                _logger.Exception(ex, "Connection with client was aborted");
            }
            return new SocketClientContextHolder(clientSocket, _logger);
        }


    }
}
