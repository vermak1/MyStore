using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class ConsoleUserInterface : IUserInterface
    {
        private readonly IUserCommandGenerator _commandGenerator;

        private readonly UserContext _userContext;

        private readonly ILogger _logger;
        public ConsoleUserInterface(UserContext context)
        {
            _userContext = context;
            _commandGenerator = new UserCommandGenerator();
            _logger = Configurator.Instance.GetLogger();
        }

        private String GetMessageFromUser()
        {
            Console.WriteLine(">");
            return Console.ReadLine();
        }

        private void ShowMessage(String message)
        {
            if(message == null)
                throw new ArgumentNullException(nameof(message));

            foreach (var line in message.Split('\n'))
            {
                if (!String.IsNullOrWhiteSpace(line))
                    Console.WriteLine("[{0}]\t{1}", DateTime.Now, line);
            }
        }

        private void ShowMessage(IEnumerable<String> message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            foreach (var line in message)
                ShowMessage(line);
        }

        private void ShowMessage(String message, params object[] args)
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
                    _logger.Exception(ex, "Processing failed within {0} method", nameof(Run));
                    ShowMessage(ex.Message);
                }
            }
            
        }

        private void ShowAvailableCommands()
        {
            EUserCommand[] commands = _userContext.GetAvailableCommands();
            String[] descrs = CommandsDescriptor.GetDescriptions(commands);
            ShowMessage("Enter one of available commands:");
            ShowMessage(descrs);
        }

        private UserCommand GetCommandFromInput()
        {
            String message = GetMessageFromUser();
            return _commandGenerator.GetUserCommandFromInput(message);
        }
    }
}