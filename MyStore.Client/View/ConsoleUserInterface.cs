using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class ConsoleUserInterface : IUserInterface
    {
        private readonly IUserMessageParser _messageParser;

        private readonly UserContext _userContext;

        public ConsoleUserInterface(UserContext context)
        {
            _userContext = context;
            _messageParser = new UserMessageParser();
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

        public async Task Run()
        {
            ShowMessage("Welcome to Store");
            while (true)
            {
                try
                {
                    ShowAvailableCommands();
                    UserCommand command = GetCommandFromInput();
                    if (command is UserExitCommand)
                    {
                        ShowMessage("Ciao");
                        return;
                    }

                    if (command is UserUnknownCommand)
                    {
                        ShowMessage("Unknown command received");
                        continue;
                    }
                    var t = await _userContext.ProcessCommand(command);
                    ShowMessage($"{t.Message}\n{t.Content}");
                }
                catch (Exception ex)
                {
                    ShowMessage(ex.Message);
                }
            }
            
        }

        public void ShowAvailableCommands()
        {
            var commands = _userContext.GetAvailableCommands();
            ShowMessage("Enter one of available commands:");
            ShowMessage(commands);
        }

        private UserCommand GetCommandFromInput()
        {
            String message = GetMessageFromUser();
            return _messageParser.GetUserCommandFromInput(message);
        }
    }
}
