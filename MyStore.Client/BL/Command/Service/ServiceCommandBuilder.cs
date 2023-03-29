using System;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class ServiceCommandBuilder
    {
        private readonly ServiceCommandFactory _factory;

        public ServiceCommandBuilder()
        {
            _factory = new ServiceCommandFactory();
        }

        public String BuildGetVersionCommand()
        {
            return _factory.GetLibraryVersionCommand();
        }
    }
}
