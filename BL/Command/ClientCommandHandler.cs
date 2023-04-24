using System;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Server
{
    internal class ClientCommandHandler
    {
        private readonly CommandParser _parser;

        public ClientCommandHandler()
        {
            _parser = new CommandParser();
        }

        public async Task<String> ParseCommandAndGenerateResponse(String command)
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
