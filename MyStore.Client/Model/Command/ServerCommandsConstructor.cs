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
            switch (command.SubType)
            {
                case EListCarsSubType.SelectAll:
                    return _factory.ListAllCarsCommand();
                case EListCarsSubType.SelectByYear:
                    return _factory.ListCarsByYear(command.Year);
                case EListCarsSubType.SelectByName:
                    return _factory.ListCarsByName(command.Model);
                case EListCarsSubType.SelectByNameAndYear:
                    return _factory.ListCarsByNameAndYear(command.Model, command.Year);
                default:
                    throw new ArgumentException("Non-existent subtype of ListCarsCommand");
            }
        }
    }
}
