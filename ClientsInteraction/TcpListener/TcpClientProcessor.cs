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

        private readonly ErrorHandler _errorHandler;

        private readonly IMessenger _messenger;

        private readonly ManualResetEvent _exitEvent;

        private readonly CancellationToken _token;
        
        private readonly ClientHelperInfo _clientHelperInfo;
        public TcpClientProcessor(TcpClient tcpClient, CancellationToken token, ICancelApplicationHelper helper)
        {
            if (tcpClient == null)
                throw new ArgumentNullException(nameof(tcpClient));

            try
            {
                _clientHelperInfo = helper.AddClient(new ClientHelperInfo(tcpClient.Client.RemoteEndPoint.ToString()));
                _exitEvent = new ManualResetEvent(true);
                _messenger = new TcpMessenger(tcpClient);
                _token = token;
                _versionChecker = new CommonLibVersionChecker(_messenger);
                _commandProcessor = new CommandProcessor();
                _errorHandler = new ErrorHandler(_messenger);
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public void Dispose()
        {
            _exitEvent?.Dispose();
            _clientHelperInfo.WorkIsEndEvent.SafeSet();
        }

        public async Task ProcessClient()
        {
            using (CancellationTokenRegistration tokenRegistration = _token.Register(WaitForEndOperationAndStop))
            {
                try
                {
                    if (!await _versionChecker.CheckVersion(_exitEvent))
                        return;
                }
                catch (Exception ex)
                {
                    await HandleException(ex);
                }
                finally
                {
                    _exitEvent.Set();
                }

                while (!_token.IsCancellationRequested && _messenger.IsConnected)
                {
                    try
                    {
                        await ReceiveCommandAndSendResponse();
                    }
                    catch (Exception ex)
                    {
                        await HandleException(ex);
                    }
                    finally 
                    { 
                        _exitEvent.Set();
                    }
                }
            }
        }

        private void WaitForEndOperationAndStop()
        {
            Log.Info("Stopping process client '{0}' due to cancellation request", _clientHelperInfo.Address);
            _exitEvent.WaitOne();
            _clientHelperInfo.WorkIsEndEvent.SafeSet();
        }

        private async Task HandleException(Exception ex)
        {
            Log.Exception(ex);
            Boolean isCustomerWaitingForResponse = !_exitEvent.WaitOne(0);
            if (isCustomerWaitingForResponse && _messenger.IsConnected)
                await _errorHandler.SendServerErrorResponse();
        }
        private async Task ReceiveCommandAndSendResponse()
        {
            String command = await ReceiveCommand();
            await ResponseToClient(command);
        }
        private async Task<String> ReceiveCommand()
        {
            String command = await _messenger.ReceiveMessageAsync();
            _exitEvent.Reset();
            Log.Info("Command {0} received from client {1}", command, _clientHelperInfo.Address);
            return command;
        }

        private async Task ResponseToClient(String command)
        {
            String response = null;
            IResult result;
            try
            {
                response = await _commandProcessor.GetResponseFromCommand(command);
                result = ResultFactory.Success();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                result = ResultFactory.InternalError();
            }

            if (result.Status == EResultStatus.Failed)
            {
                await _errorHandler.SendServerErrorResponse(result.Message);
                return;
            }

            await _messenger.SendMessageAsync(response);
            Log.Info("Response sent to client {0}", _clientHelperInfo.Address);
        }
    }
}
