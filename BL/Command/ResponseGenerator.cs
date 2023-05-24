using System;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Server
{
    internal class ResponseGenerator
    {
        private readonly ResponseFactory _factory;

        public ResponseGenerator()
        {
            _factory = new ResponseFactory();
        }
        public async Task<String> GenerateResponse(CommandInfo info)
        {
            switch (info)
            {
                case ListCarCommand command:
                    ListCarsCommandHandler handler = new ListCarsCommandHandler();
                    return await handler.GetCars(command);
                default:
                    return _factory.UnknownCommand("Unknown command received");
            }
            
        }
    }
}
