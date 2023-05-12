using System;

namespace MyStore.Client
{
    internal static class UserCommandFactory
    {
        public static UserCommand CreateUserCommand(Tuple<String, String[]> commandTypeAndArgs)
        {
            EUserCommand commandType = CommandTypeMapper.GetCommandType(commandTypeAndArgs.Item1);
            switch (commandType)
            {
                case EUserCommand.Exit:
                    return ExitCommand();
                case EUserCommand.ListAllCars:
                    return ListCarsCommand(commandTypeAndArgs.Item2);
                case EUserCommand.Login:
                    return LoginCommand(commandTypeAndArgs.Item2);
                case EUserCommand.Logoff:
                    return LogoffCommand();
                default:
                    return UnknownCommand();
            }
        }

        private static UserExitCommand ExitCommand()
        {
            return new UserExitCommand();
        }

        private static UserListAllCarsCommand ListCarsCommand(String[] args)
        {
            if (args.Length == 0) 
                return new UserListAllCarsCommand();

            throw new NotImplementedException();
        }

        private static UserUnknownCommand UnknownCommand()
        {
            return new UserUnknownCommand();
        }

        private static UserLoginCommand LoginCommand(String[] loginAndPass)
        {
            return new UserLoginCommand();
        }

        private static UserLogoffCommand LogoffCommand()
        {
            return new UserLogoffCommand();
        }
    }
}
