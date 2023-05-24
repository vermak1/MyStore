using System;
using System.Linq;

namespace MyStore.Client
{
    internal class UserCommandGenerator : IUserCommandGenerator
    {
        public UserCommand GetUserCommandFromInput(String input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            Tuple<String, String[]> commandTypeAndArgs = GetCommandTuple(input);
            return UserCommandFactory.CreateUserCommand(commandTypeAndArgs);
        }

        private String ParseCommandType(String input)
        {
            return input.ToLower().Split(' ')[0];
        }

        private String[] ParseCommandArguments(String input) 
        {
            return input.Trim().ToLower().Split(' ').Skip(1).ToArray();
        }

        private Tuple<String, String[]> GetCommandTuple(String input)
        {
            return new Tuple<String, String[]>(
                ParseCommandType(input),
                ParseCommandArguments(input));
        }
    }
}
