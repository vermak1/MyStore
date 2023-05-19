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

        public String GetServerCommandForListAllCars(UserListAllCarsCommand command)
        {
            switch (command.Filter)
            {
                case EListCarsFilter.SelectAll:
                    return _factory.ListAllCarsCommand();
                case EListCarsFilter.SelectByYear:
                    return _factory.ListCarsByYear(command.Year);
                case EListCarsFilter.SelectByName:
                    return _factory.ListCarsByName(command.Model);
                case EListCarsFilter.SelectByNameAndYear:
                    return _factory.ListCarsByNameAndYear(command.Model, command.Year);
                default:
                    throw new ArgumentException("Non-existent subtype of ListCarsCommand");
            }
        }
    }
}
