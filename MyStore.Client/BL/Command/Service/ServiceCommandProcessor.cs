using System;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class ServiceCommandProcessor
    {
        private readonly ServiceCommandParser _parser;

        public ServiceCommandProcessor()
        {
            _parser = new ServiceCommandParser();
        }

        public Int32 GetServerVersion(String response)
        {
            LibVersionCommand command = _parser.GetLibVersionCommand(response);
            return command.Version;
        }
    }
}
