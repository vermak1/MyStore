using System;

namespace MyStore.Client
{
    internal static class UserCommandFactory
    {
        public static UserExitCommand ExitCommand()
        {
            return new UserExitCommand();
        }

        public static UserCommand ListCarsCommand() 
        {
            return new UserListAllCarsCommand();
        }

        public static UserCommand UnknownCommand()
        {
            return new UserUnknownCommand();
        }

        public static UserCommand LoginCommand()
        {
            return new UserLoginCommand();
        }

        public static UserCommand LogoffCommand()
        {
            return new UserLogoffCommand();
        }
    }
}
