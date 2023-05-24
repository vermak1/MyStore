using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class TcpClientProcessor : IClientProcessor
    {
        private readonly CommandProcessor _commandProcessor;

        private readonly CommonLibVersionChecker _versionChecker;

        private readonly IMessenger _messenger;

        private readonly ManualResetEvent _exitEvent;

        private readonly CancellationToken _token;
        public TcpClientProcessor(TcpClient tcpClient, CancellationToken token)
        {
            _token = token;
            _messenger = new TcpMessenger(tcpClient);
            _versionChecker = new CommonLibVersionChecker(_messenger);
            _commandProcessor = new CommandProcessor();
            _exitEvent = new ManualResetEvent(true);
        }

        public void Dispose()
        {
            _messenger?.Dispose();
            _exitEvent?.Dispose();
        }
        public override string ToString()
        {
            return _messenger.ToString();
        }
        public async Task ProcessClient()
        {
            using (CancellationTokenRegistration tokenRegistration = _token.Register(WaitForEndOperationAndStop))
            {
                try
                {
                    if (!await _versionChecker.CheckVersion())
                        return;
                
                    while(!_token.IsCancellationRequested)
                    {
                        String command = await ReceiveCommand();
                        await ResponseToClient(command);
                    }
                }
                catch
                {
                    _token.ThrowIfCancellationRequested();
                    throw;
                }
            }
        }

        private void WaitForEndOperationAndStop()
        {
            Log.Info("Stopping process client '{0}' due to cancellation request", _messenger);
            _exitEvent.WaitOne();
            Dispose();
        }

        private async Task ResponseToClient(String command)
        {
            String response = await _commandProcessor.GetResponseFromCommand(command);
            await _messenger.SendMessageAsync(response);
            Log.Info("Response sent to client {0}", _messenger);
            _exitEvent.Set();
        }

        private async Task<String> ReceiveCommand()
        {
            String command = await _messenger.ReceiveMessageAsync();
            _exitEvent.Reset();
            Log.Info("Command {0} received from client {1}", command, _messenger);
            return command;
        }
    }
}
