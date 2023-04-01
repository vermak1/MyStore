using System;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class InitialProcessor
    {
        private readonly ILogger _logger;

        private readonly IMessenger _messenger;

        private readonly IConnectionHolder _connectionHolder;

        private readonly IUserInterface _userInterface;

        private readonly ServiceCommandBuilder _commandProvider;

        private readonly ServiceCommandProcessor _commandProcessor;

        public InitialProcessor(ILogger logger, IMessenger messenger, IConnectionHolder connectionHolder, IUserInterface userInterface)
        {
            try
            {
                _logger = logger;
                _messenger = messenger;
                _connectionHolder = connectionHolder;
                _userInterface = userInterface;
                _commandProvider = new ServiceCommandBuilder();
                _commandProcessor = new ServiceCommandProcessor();
            }
            catch(Exception ex)
            {
                _connectionHolder?.Dispose();
                _logger.Exception(ex, "Failed to initialize {0}", nameof(InitialProcessor));
                throw;
            }
        }

        public async Task<Boolean> InitialConnect()
        {
            _userInterface.ShowMessage("Welcome to store");

            try
            {
                Boolean connected = await TryConnectAsync();
                if (!connected)
                {
                    return false;
                }

                var versionMatch = await CheckLibraryVersion();
                if (!versionMatch)
                {
                    _userInterface.ShowMessage("Current client's version doesn't match with server version");
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
                _userInterface.ShowMessage("Connecting to server...");
                Boolean success = await _connectionHolder.ConnectToServerAsync();
                if (!success)
                {
                    _userInterface.ShowMessage("Failed to establish connection to server");
                    return false;
                }
                
            }
            catch
            {
                throw;
            }
            _userInterface.ShowMessage("Connection is successfull");
            return true;
        }

        private async Task<Boolean> CheckLibraryVersion()
        {
            Int32 clientVersion = LibraryInfo.Version;

            String command = _commandProvider.BuildGetVersionCommand();
            await _messenger.SendMessageAsync(command);

            String response = await _messenger.ReceiveMessageAsync();
            Int32 serverVersion = _commandProcessor.GetServerVersion(response);

            _logger.Info("Client version: [{0}], server version: [{1}]", clientVersion, serverVersion);
            return clientVersion == serverVersion;
        }
    }
}
