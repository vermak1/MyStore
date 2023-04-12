using System;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class ServerCommandsConstructor
    {
        private readonly CommandFactory _factory;
        public ServerCommandsConstructor()
        {
            _factory = new CommandFactory();
        }

        public String GetServerCommandForListAllCars()
        {
            return _factory.ListAllCarsCommand();
        }
    }
}
