using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class ConsoleUserInterface : IUserInterface
    {
        private readonly IUserCommandGenerator _commandGenerator;

        private readonly UserContext _userContext;

        public ConsoleUserInterface(UserContext context)
        {
            _userContext = context;
            _commandGenerator = new UserCommandGenerator();
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
            while (!_userContext.IsExitRequested)
            {
                try
                {
                    ShowAvailableCommands();
                    UserCommand command = GetCommandFromInput();
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
            return _commandGenerator.GetUserCommandFromInput(message);
        }
    }
}