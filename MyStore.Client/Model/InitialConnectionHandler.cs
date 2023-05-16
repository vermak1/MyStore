using System;
using System.Net;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class InitialConnectionHandler
    {
        private readonly ILogger _logger;

        private readonly IMessenger _messenger;

        private readonly ServiceCommandBuilder _commandProvider;

        private readonly ServiceCommandProcessor _commandProcessor;

        public InitialConnectionHandler(IMessenger messenger)
        {
            _logger = Configurator.Instance.GetLogger();
            _messenger = messenger;
            _commandProvider = new ServiceCommandBuilder();
            _commandProcessor = new ServiceCommandProcessor();
        }

        public async Task TryConnectIfNeeded()
        {
            if (await _messenger.ConnectToServerAsync())
                await CheckLibraryVersion();
        }

        private async Task CheckLibraryVersion()
        {
            Int32 clientVersion = LibraryInfo.Version;

            String command = _commandProvider.BuildGetVersionCommand();

            String response = await _messenger.SendAndReceiveMessageAsync(command);
            Int32 serverVersion = _commandProcessor.GetServerVersion(response);

            _logger.Info("Client version: [{0}], server version: [{1}]", clientVersion, serverVersion);
            if (clientVersion != serverVersion)
                throw new Exception(String.Format("Common library version mismatch\nClient version: [{0}], server version: [{1}]", clientVersion, serverVersion));
        }
    }
}
