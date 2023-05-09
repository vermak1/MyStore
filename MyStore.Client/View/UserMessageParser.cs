using System;

namespace MyStore.Client
{
    internal class UserMessageParser : IUserMessageParser
    {
        public UserCommand GetUserCommandFromInput(String input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            String command = ParseCommandType(input);

            switch(command)
            {
                case "exit":
                    return UserCommandFactory.ExitCommand();
                case "listallcars":
                    return UserCommandFactory.ListCarsCommand();
                case "login":
                    return UserCommandFactory.LoginCommand();
                case "logoff":
                    return UserCommandFactory.LogoffCommand();
                default:
                    return UserCommandFactory.UnknownCommand();
            }
        }

        private String ParseCommandType(String input)
        {
            return input.ToLower().Split(' ')[0];
        }
    }
}
