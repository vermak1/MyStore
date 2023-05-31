using System;
using System.Threading;
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

        public async Task<Boolean> CheckVersion(ManualResetEvent exitEvent)
        {
            var request = await _messenger.ReceiveMessageAsync();
            exitEvent.Reset();
            Int32 serverVersion = LibraryInfo.Version;
            Boolean isServiceCommandCorrect = _serviceCommandProcessor.TryGetLibVersionCommand(request, out Int32 clientVersion);
            if (!isServiceCommandCorrect)
            {
                Log.Error("Wrong command provided: [{0}]", request);
                return false;
            }

            String responseWithVersion = _serviceCommandBuider.BuildReturnVersionCommand();
            await _messenger.SendMessageAsync(responseWithVersion);
            if (serverVersion != clientVersion)
            {
                Log.Error("Common dll version mismatch: server version: {0}, client '{1}' version: {2}", serverVersion, _messenger.Address, clientVersion);
                return false;
            }
            Log.Info("Common dll versions of server and client '{0}' are matched, version '{1}'", _messenger.Address, serverVersion);
            return true;
        }
    }
}
