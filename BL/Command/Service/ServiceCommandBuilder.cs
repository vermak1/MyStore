using System;
using MyStore.CommonLib;

namespace MyStore.Server
{
    internal class ServiceCommandBuilder
    {
        private readonly ServiceCommandFactory _factory;

        public ServiceCommandBuilder()
        {
            _factory = new ServiceCommandFactory();
        }

        public String BuildReturnVersionCommand()
        {
            return _factory.ReturnLibraryVersionCommand();
        }
    }
}
