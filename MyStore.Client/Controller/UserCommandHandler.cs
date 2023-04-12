using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class UserCommandHandler
    {
        private readonly ServerCommandsConstructor _commandsConstructor;

        private readonly IMessenger _messenger;

        private readonly ResponseProcessor _responseProcessor;

        public UserCommandHandler(IMessenger messenger)
        {
            _commandsConstructor = new ServerCommandsConstructor();
            _messenger = messenger;
            _responseProcessor = new ResponseProcessor();
        }

        public async Task<String> HandleListAllCarsCommand()
        {
            String message = _commandsConstructor.GetServerCommandForListAllCars();
            String response = await _messenger.SendAndReceiveMessageAsync(message);
            return _responseProcessor.ProcessResponse(response);
        }
    }
}
