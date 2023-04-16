using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class ConsoleUserInterface : IUserInterface
    {
        public IUserMessageParser MessageParser { get; }

        public ConsoleUserInterface()
        {
            MessageParser = new UserMessageParser();
        }

        public String GetMessageFromUser()
        {
            Console.WriteLine(">");
            return Console.ReadLine();
        }

        public void ShowMessage(String message)
        {
            if(message == null)
                throw new ArgumentNullException(nameof(message));

            Console.WriteLine("[{0}]\t{1}", DateTime.Now, message);
        }

        public void ShowMessage(IEnumerable<String> message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            foreach (var line in message)
                ShowMessage(line);
        }

        public void ShowMessage(String message, params object[] args)
        {
            if (args == null)
            {
                ShowMessage(message);
                return;
            }

            String formatted = String.Format(message, args);
            ShowMessage(formatted);
        }

        public async Task Run(IController controller)
        {
            ShowMessage("Welcome to Store");
            UserCommand command = null;
            do
            {
                try
                {
                    ShowAvailableCommands();
                    command = GetCommandFromInput();
                    switch (command)
                    {
                        case UserListAllCarsCommand c:
                            var response = await controller.GetAllCarsCommand(c);
                            ShowMessage(response.ToString());
                            break;

                        case UserExitCommand _:
                            ShowMessage("ciao");
                            break;

                        case UserUnknownCommand _:
                            ShowMessage("Unknown command received");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(ex.Message);
                }
            } while (!(command is UserExitCommand));
        }

        public void ShowAvailableCommands()
        {
            var commands = UserCommandsProvider.GetAvailableCommands();
            ShowMessage("Enter one of available commands:");
            ShowMessage(commands);
        }

        private UserCommand GetCommandFromInput()
        {
            String message = GetMessageFromUser();
            return MessageParser.GetUserCommandFromInput(message);
        }
    }
}
