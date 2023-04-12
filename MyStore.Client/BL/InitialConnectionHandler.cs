using System;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class InitialConnectionHandler
    {
        public event EventHandler<ErrorEventArgs> ErrorOccured;

        public event EventHandler<EventArgs> ConnectedToServer;

        private readonly ILogger _logger;

        private readonly IMessenger _messenger;

        private readonly ServiceCommandBuilder _commandProvider;

        private readonly ServiceCommandProcessor _commandProcessor;

        public InitialConnectionHandler(ILogger logger, IMessenger messenger)
        {
            try
            {
                _logger = logger;
                _messenger = messenger;
                _commandProvider = new ServiceCommandBuilder();
                _commandProcessor = new ServiceCommandProcessor();
            }
            catch(Exception ex)
            {
                _logger.Exception(ex, "Failed to initialize {0}", nameof(InitialConnectionHandler));
                throw;
            }
        }

        public async Task<Boolean> InitialConnect()
        {
            try
            {
                Boolean connected = await TryConnectAsync();
                if (!connected)
                {
                    OnErrorOccured(null, "Failed to connect to server");
                    return false;
                }

                var versionMatch = await CheckLibraryVersion();
                if (!versionMatch)
                {
                    OnErrorOccured(null, "Current client's version doesn't match with server version");
                    _logger.Error("Client and server versions of the CommonLib are different");
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.Exception(ex, "Error during Initial Connect");
                throw;
            }

            return true;
        }

        private async Task<Boolean> TryConnectAsync()
        {
            try
            {
                Boolean success = await _messenger.ConnectToServerAsync();
                if (!success)
                    return false;
            }
            catch(Exception ex)
            {
                OnErrorOccured(ex, "Failed to connect to server");
                _logger.Exception(ex, "Connection is failed");
                throw;
            }
            OnConnectedToServer();
            return true;
        }

        private async Task<Boolean> CheckLibraryVersion()
        {
            Int32 clientVersion = LibraryInfo.Version;

            String command = _commandProvider.BuildGetVersionCommand();

            String response = await _messenger.SendAndReceiveMessageAsync(command);
            Int32 serverVersion = _commandProcessor.GetServerVersion(response);

            _logger.Info("Client version: [{0}], server version: [{1}]", clientVersion, serverVersion);
            return clientVersion == serverVersion;
        }

        protected virtual void OnErrorOccured(Exception ex, String message)
        {
            ErrorOccured?.Invoke(this, new ErrorEventArgs(ex, message));
        }

        protected virtual void OnConnectedToServer()
        {
            ConnectedToServer?.Invoke(this, new EventArgs());
        }
    }
}
