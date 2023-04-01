using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class MainProcessor
    {
        private readonly ILogger _logger;

        private readonly IUserInterface _userInterface;

        private readonly IConnectionHolder _connectHolder;

        private readonly IMessenger _messenger;

        private readonly Configurator _configurator;

        private readonly CommandsConstructor _construstor;

        private readonly ResponseProcessor _responseProcessor;

        private readonly InitialProcessor _initialProcessor;
        public MainProcessor()
        {
            try
            {
                _configurator = new Configurator("NonExistentPath.txt");
                _logger = _configurator.GetLogger();
                _connectHolder = _configurator.GetConnectHolder();
                _messenger = _configurator.GetMessenger(_connectHolder);
                _userInterface = _configurator.GetUserInterface();
                _construstor = new CommandsConstructor();
                _responseProcessor = new ResponseProcessor();
                _initialProcessor = new InitialProcessor(_logger, _messenger, _connectHolder, _userInterface);
            }
            catch(Exception ex)
            {
                _logger?.Exception(ex, "Failed to start application");
                _connectHolder?.Dispose();
                throw;
            }
        }

        public async Task Start()
        {
            try
            {
                using (_connectHolder)
                {
                    Boolean success = await _initialProcessor.InitialConnect();
                    if (!success)
                        return;

                    await MainCycle();
                }
            }
            catch (Exception ex)
            {
                _userInterface?.ShowMessage(String.Format("Failed to procceed"));
                _logger?.Exception(ex, "Error within Start method occured");
                throw;
            }
        }

        private async Task MainCycle()
        {
            try
            {
                String input = "";
                while (input.ToLower() != "exit")
                {
                    ShowAvailableCommands();
                    input = _userInterface.GetMessageFromUser();

                    Boolean success = _construstor.TryConstructNewCommand(input, out String command);

                    if (!success)
                    {
                        _userInterface.ShowMessage(String.Format("Input \"{0}\" does not correspond to any possible command, try again.", input));
                        _logger.Warning("Input \"{0}\" does not correspond to any possible command", input);
                        continue;
                    }

                    await _messenger.SendMessageAsync(command);
                    String response = await _messenger.ReceiveMessageAsync();

                    String responseToUi = _responseProcessor.ProcessResponse(response);
                    _userInterface.ShowMessage(responseToUi);
                }
            }
            catch (Exception ex)
            {
                _logger.Exception(ex, "Main cycle is failed");
                throw;
            }
        }

        private void ShowAvailableCommands()
        {
            var commands = CommandsProvider.GetAvailableCommands();
            _userInterface.ShowMessage("Enter one of available commands:");
            _userInterface.ShowMessage(commands);
        }
    }
}
