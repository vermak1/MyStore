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
            if (args.Length > 2)
                throw new ArgumentException("Too many parameters provided for the command");

            ListCarsCommandBuilder builder = new ListCarsCommandBuilder();
            if (args.Length == 1)
            {
                if (Int32.TryParse(args[0], out Int32 year))
                {
                    return builder.Year(year)
                        .SubType(EListCarsFilter.SelectByYear)
                        .Build();
                }

                return builder.Model(args[0])
                        .SubType(EListCarsFilter.SelectByName)
                        .Build();
            }

            if (args.Length == 2)
            {
                if (!Int32.TryParse(args[1], out Int32 year))
                    throw new ArgumentException(String.Format("Failed to convert '{0}' to integer", args[1]));

                return builder.Model(args[0])
                    .Year(year)
                    .SubType(EListCarsFilter.SelectByNameAndYear)
                    .Build();
            }
            return builder.SubType(EListCarsFilter.SelectAll)
                .Build();
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
