using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class MainProcessor : IDisposable
    {
        private readonly ILogger _logger;

        private readonly IUserInterface _userInterface;

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
                _messenger = _configurator.GetMessenger();
                _userInterface = _configurator.GetUserInterface();
                _construstor = new CommandsConstructor();
                _responseProcessor = new ResponseProcessor();
                _initialProcessor = new InitialProcessor(_logger, _messenger, _userInterface);
            }
            catch(Exception ex)
            {
                _messenger?.Dispose();
                _logger?.Exception(ex, "Failed to start application");
                throw;
            }
        }

        public void Dispose()
        {
            _messenger?.Dispose();
        }

        public async Task Start()
        {
            try
            {
                Boolean success = await _initialProcessor.InitialConnect();
                if (!success)
                    return;

                await MainCycle();
            }
            catch (Exception ex)
            {
                _userInterface?.ShowMessage("Failed to procceed");
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
                        _userInterface.ShowMessage("Input \"{0}\" does not correspond to any possible command, try again.", input);
                        _logger.Warning("Input \"{0}\" does not correspond to any possible command", input);
                        continue;
                    }

                    String response = await _messenger.SendAndReceiveMessageAsync(command);

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
