using System;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class CommandsConstructor
    {
        private readonly CommandFactory _factory;
        public CommandsConstructor()
        {
            _factory = new CommandFactory();
        }

        public Boolean TryConstructNewCommand(String input, out String command)
        {
            command = String.Empty;

            Boolean success = TryGetCommandFromString(input, out ECommandType commandType);
            if (!success)
            {
                return false;
            }

            switch (commandType)
            {
                case ECommandType.ListAllCars:
                    command = _factory.ListAllCarsCommand();
                    return true;

                default:
                    return false;
            }
        }

        private Boolean TryGetCommandFromString(String input, out ECommandType command)
        {
            switch(input.ToLower())
            {
                case "listallcars":
                    command = ECommandType.ListAllCars;
                    return true;

                default:
                    command = ECommandType.Login;
                    return false;
            }
        }
    }
}
