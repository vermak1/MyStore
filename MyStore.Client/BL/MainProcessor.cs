using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class MainProcessor : IDisposable
    {
        public event EventHandler<ErrorEventArgs> ErrorOccured;

        private readonly ILogger _logger;

        private readonly IUserInterface _userInterface;

        private readonly IMessenger _messenger;

        private readonly Configurator _configurator;

        private readonly InitialConnectionHandler _initConnectionHandler;

        private readonly Controller _controller;
        public MainProcessor()
        {
            try
            {
                _configurator = new Configurator();
                _logger = _configurator.GetLogger();
                _messenger = _configurator.GetMessenger();
                _userInterface = _configurator.GetUserInterface();
                _initConnectionHandler = new InitialConnectionHandler(_logger, _messenger);
                _controller = new Controller(_logger, _userInterface, _messenger);

                ErrorOccured += _userInterface.OnErrorOccured;
                _controller.UserCommandHandled += _userInterface.OnCommandHandled;
                _initConnectionHandler.ErrorOccured += _userInterface.OnErrorOccured;
                _initConnectionHandler.ConnectedToServer += _userInterface.OnConnectedToServer;
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
            _userInterface.ShowMessage("Welcome to store");
            try
            {
                Boolean success = await _initConnectionHandler.InitialConnect();
                if (!success)
                    return;

                await _controller.Run();
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex, "Application failed within main method");
                _logger?.Exception(ex, "Error within Start method occured");
                throw;
            }
        }

        protected virtual void OnErrorOccured(Exception ex, String message)
        {
            ErrorOccured?.Invoke(this, new ErrorEventArgs(ex, message));
        }
    }
}
