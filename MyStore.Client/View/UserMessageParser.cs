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
                    return UserCommandFactory.CreateExitCommand();
                case "listallcars":
                    return UserCommandFactory.CreateListCarsCommand();
                default:
                    return UserCommandFactory.CreateUnknownCommand();
            }
        }

        private String ParseCommandType(String input)
        {
            return input.ToLower().Split(' ')[0];
        }
    }
}
