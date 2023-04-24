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

        public Boolean TryGetLibVersionCommand(String request, out Int32 version)
        {
            var command = _parser.GetLibVersionCommand(request);
            version = command.Version;
            if (command.ServiceCommand != EServiceCommand.GetLibraryVersion)
                return false;

            return true;
        }
    }
}
