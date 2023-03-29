using System;
using MyStore.CommonLib;

namespace MyStore.Server
{
    internal class ServiceCommandProcessor
    {
        private readonly ServiceCommandParser _parser;

        public ServiceCommandProcessor()
        {
            _parser = new ServiceCommandParser();
        }

        public Boolean TryGetLibVersionCommand(String request)
        {
            var command = _parser.GetLibVersionCommand(request);
            if (command.ServiceCommand != EServiceCommand.GetLibraryVersion)
                return false;

            return true;
        }
    }
}
