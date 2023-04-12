using System;

namespace MyStore.Client
{
    internal static class UserCommandFactory
    {
        public static ExitCommand CreateExitCommand()
        {
            return new ExitCommand()
            {
                CommandType = EUserCommand.Exit,
            };
        }

        public static UserCommand CreateListCarsCommand() 
        {
            return new UserListAllCarsCommand()
            {
                CommandType = EUserCommand.ListAllCars
            };
        }

        public static UserCommand CreateUnknownCommand()
        {
            return new UserUnknownCommand()
            {
                CommandType = EUserCommand.Unknown
            };
        }
    }
}
