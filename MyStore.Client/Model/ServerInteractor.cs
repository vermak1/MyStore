using MyStore.CommonLib;
using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class ServerInteractor : IServerInteractor
    {
        private readonly ServerCommandsConstructor _commandsConstructor;

        private readonly IMessenger _messenger;

        private readonly ResponseConverter _responseConverter;

        private readonly InitialConnectionHandler _initialConnectionHandler;

        private readonly ILogger _logger;
            
        public ServerInteractor()
        {
            try
            {
                _messenger = Configurator.Instance.GetMessenger();
                _logger = Configurator.Instance.GetLogger();
                _initialConnectionHandler = new InitialConnectionHandler(_messenger);
                _commandsConstructor = new ServerCommandsConstructor();
                _responseConverter = new ResponseConverter();
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

        public async Task<ListCarsResponseInfo> GetListCars(UserListAllCarsCommand command)
        {
            await _initialConnectionHandler.TryConnectIfNeeded();
            var request = _commandsConstructor.GetServerCommandForListAllCars(command);
            var response = await _messenger.SendAndReceiveMessageAsync(request);
            return _responseConverter.ConvertStringToListCars(response);
        }
    }
}
