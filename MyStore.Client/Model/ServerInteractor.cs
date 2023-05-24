using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class ServerInteractor : IServerInteractor
    {
        private readonly IMessenger _messenger;

        private readonly InitialConnectionHandler _initialConnectionHandler;

        private readonly ILogger _logger;
        
        public ServerInteractor()
        {
            try
            {
                _messenger = Configurator.Instance.GetMessenger();
                _logger = Configurator.Instance.GetLogger();
                _initialConnectionHandler = new InitialConnectionHandler(_messenger);
            }
            catch(Exception ex)
            {
                _messenger?.Dispose();
                _logger?.Exception(ex, "Failed to initialize {0}", nameof(ServerInteractor));
                throw;
            }
        }

        public void Dispose()
        {
            _messenger?.Dispose();
        }

        public async Task<String> SendCommandAndReceiveResponse(String command)
        {
            await _initialConnectionHandler.TryConnectIfNeeded();
            return await _messenger.SendAndReceiveMessageAsync(command);
        }
    }
}
