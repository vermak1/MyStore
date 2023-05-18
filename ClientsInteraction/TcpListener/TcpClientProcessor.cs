using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class TcpClientProcessor : IClientProcessor
    {
        private readonly CommandProcessor _commandProcessor;

        private readonly CommonLibVersionChecker _versionChecker;

        public TcpClientProcessor(TcpClient tcpClient)
        {
            IMessenger messenger = new TcpMessenger(tcpClient);
            _versionChecker = new CommonLibVersionChecker(messenger);
            _commandProcessor = new CommandProcessor(messenger);
        }

        public async Task ProcessClient()
        {
            try
            {
                if (!await _versionChecker.CheckVersion())
                    return;

                Boolean commandProcessed = await _commandProcessor.WaitRequestAndResponse();
                while (commandProcessed)
                {
                    commandProcessed = await _commandProcessor.WaitRequestAndResponse();
                }
            }
            catch (Exception ex) 
            {
                Log.Exception(ex, "Failed to process client");
                throw;
            }
        }
    }
}
