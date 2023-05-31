using System;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Server
{
    internal class ErrorHandler
    {
        private readonly IMessenger _messenger;

        private readonly ResponseFactory _factory;
        public ErrorHandler(IMessenger messenger)
        {
            _messenger = messenger;
            _factory = new ResponseFactory();
        }

        public async Task SendServerErrorResponse(String message)
        {
            String command = _factory.ServerError(message);
            await _messenger.SendMessageAsync(command);
        }

        public async Task SendServerErrorResponse()
        {
            await SendServerErrorResponse("Internal server error");
        }
    }
}
