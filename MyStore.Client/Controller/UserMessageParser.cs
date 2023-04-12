using System;

namespace MyStore.Client
{
    internal static class UserMessageParser
    {
        public static UserCommand GetUserCommandFromInput(String input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            String command = ParseCommandType(input);

            switch(command)
            {
                case "exit":
                    return UserCommandFactory.CreateExitCommand();
                case "listallcars":
                    return UserCommandFactory.CreateListCarsCommand();
                default:
                    return UserCommandFactory.CreateUnknownCommand();
            }
        }

        private static String ParseCommandType(String input)
        {
            return input.ToLower().Split(' ')[0];
        }
    }
}
