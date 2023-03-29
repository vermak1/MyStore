using System;
using MyStore.CommonLib;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class CommandProcessor
    {
        private readonly CommandParser _parser;

        public CommandProcessor()
        {
            _parser = new CommandParser();
        }
        public async Task<String> HandleRequest(String command)
        {
            CommandInfo commandInfo = _parser.GetCommandInfo(command);

            switch (commandInfo.CommandType)
            {
                case ECommandType.ListAllCars:
                    ListCarsCommandHandler handler = new ListCarsCommandHandler();
                    return await handler.GenerateResponse();


                default:
                    return "Unknown command";
            }
        }
    }
}
