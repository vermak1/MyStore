using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class CommandProcessor
    {
        private readonly ClientCommandHandler _handler;

        private readonly IMessenger _messenger;
        public CommandProcessor(IMessenger messenger)
        {
            _handler = new ClientCommandHandler();
            _messenger = messenger;
        }

        public async Task<Boolean> WaitRequestAndResponse()
        {
            try
            {
                var command = await _messenger.ReceiveMessageAsync();
                var t = _handler.ParseCommandAndGenerateResponse(command);
                Log.Info("Command [{0}] was requested", command);

                String response = await t;
                await _messenger.SendMessageAsync(response);
                return true;
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Failed to wait and response to client");
                return false;
            }
        }
    }
}
