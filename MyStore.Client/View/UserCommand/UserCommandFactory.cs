using System;

namespace MyStore.Client
{
    internal static class UserCommandFactory
    {
        public static UserExitCommand CreateExitCommand()
        {
            return new UserExitCommand()
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
