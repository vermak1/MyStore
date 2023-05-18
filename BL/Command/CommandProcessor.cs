using MyStore.CommonLib;
using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class CommandProcessor
    {
        private readonly ResponseGenerator _responseGenerator;

        private readonly IMessenger _messenger;

        private readonly CommandParser _parser;
        public CommandProcessor(IMessenger messenger)
        {
            _responseGenerator = new ResponseGenerator();
            _messenger = messenger;
            _parser = new CommandParser();
        }

        public async Task<Boolean> WaitRequestAndResponse()
        {
            try
            {
                String command = await _messenger.ReceiveMessageAsync();
                Log.Info("Command [{0}] received", command);
                CommandInfo info = _parser.GetCommandInfo(command);
                var response = await _responseGenerator.GenerateResponse(info);
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
