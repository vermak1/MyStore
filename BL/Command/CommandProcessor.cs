using MyStore.CommonLib;
using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class CommandProcessor
    {
        private readonly ResponseGenerator _responseGenerator;

        private readonly CommandParser _parser;
        public CommandProcessor()
        {
            _responseGenerator = new ResponseGenerator();
            _parser = new CommandParser();
        }

        public async Task<String> GetResponseFromCommand(String command)
        {
            CommandInfo info = _parser.GetCommandInfo(command);
            return await _responseGenerator.GenerateResponse(info);
        }
    }
}
