using System;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Server
{
    internal class CommonLibVersionChecker
    {
        private readonly IMessenger _messenger;

        private readonly ServiceCommandProcessor _serviceCommandProcessor;

        private readonly ServiceCommandBuilder _serviceCommandBuider;

        public CommonLibVersionChecker(IMessenger messenger)
        {
            _messenger = messenger;
            _serviceCommandBuider = new ServiceCommandBuilder();
            _serviceCommandProcessor = new ServiceCommandProcessor();
        }

        public async Task<Boolean> CheckVersion()
        {
            var request = await _messenger.ReceiveMessageAsync();
            Int32 serverVersion = LibraryInfo.Version;
            Boolean correctServiceCommand = _serviceCommandProcessor.TryGetLibVersionCommand(request, out Int32 clientVersion);
            if (!correctServiceCommand)
            {
                Log.Error("Wrong command provided: [{0}]", request);
                return false;
            }

            String responseWithVersion = _serviceCommandBuider.BuildReturnVersionCommand();
            await _messenger.SendMessageAsync(responseWithVersion);
            if (serverVersion != clientVersion)
            {
                Log.Error("Common dll version mismatch: server version: {0}, client version: {1}", serverVersion, clientVersion);
                return false;
            }
            Log.Info("Common dll versions of server and client are matched, version '{0}'", serverVersion);
            return true;
        }
    }
}
