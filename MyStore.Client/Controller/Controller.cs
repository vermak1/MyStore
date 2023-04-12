using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class Controller
    {
        public event EventHandler<UserCommandHandledArgs> UserCommandHandled;

        private readonly IUserInterface _userInterface;

        private readonly ILogger _logger;

        private readonly UserCommandHandler _userCommandHandler;
        public Controller(ILogger logger, IUserInterface userInterface, IMessenger messenger)
        {
            _logger = logger;
            _userInterface = userInterface;
            _userCommandHandler = new UserCommandHandler(messenger);
        }

        public async Task Run()
        {
            try
            {
                UserCommand command;
                do
                {
                    String output = String.Empty;
                    ShowAvailableCommands();
                    command = GetCommandFromInput();
                    switch (command.CommandType)
                    {
                        case EUserCommand.ListAllCars:
                            if (command is UserListAllCarsCommand)
                                 output = await _userCommandHandler.HandleListAllCarsCommand();
                            break;

                        case EUserCommand.Exit:
                            output = "ciao";
                            break;

                        case EUserCommand.Unknown:
                            output = "Unknown command";
                            break;
                    }
                    OnCommandHandled(output, command.CommandType);
                } while (command.CommandType != EUserCommand.Exit);
            }
            catch (Exception ex) 
            {
                _logger.Exception(ex, "Error within {0} cycle occured", nameof(Run));
                throw;
            }
        }

        protected virtual void OnCommandHandled(String message, EUserCommand command)
        {
            UserCommandHandled?.Invoke(this, new UserCommandHandledArgs(message, command));
        }

        private UserCommand GetCommandFromInput()
        {
            String message = _userInterface.GetMessageFromUser();
            return UserMessageParser.GetUserCommandFromInput(message);
        }

        private void ShowAvailableCommands()
        {
            var commands = UserCommandsProvider.GetAvailableCommands();
            _userInterface.ShowMessage("Enter one of available commands:");
            _userInterface.ShowMessage(commands);
        }
    }
}
