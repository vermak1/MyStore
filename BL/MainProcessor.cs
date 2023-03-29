using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class MainProcessor
    {
        private readonly ILogger _logger;

        private readonly IClientAwaiter _awaiter;

        private readonly ServerSocketConnectionHolder _serverConnection;

        private readonly ServiceCommandProcessor _serviceCommandProcessor;

        private readonly ServiceCommandBuilder _serviceCommandBuider;

        private readonly CommandProcessor _commandProcessor;

        public MainProcessor()
        {
            try
            {
                _logger = new ServerLogger("MyStore.Server.log");
                _serverConnection = new ServerSocketConnectionHolder(_logger);
                _awaiter = new SocketClientAwaiter(_serverConnection.ServerSocket, _logger);
                _serviceCommandBuider = new ServiceCommandBuilder();
                _serviceCommandProcessor = new ServiceCommandProcessor();
                _commandProcessor = new CommandProcessor();
            }
            catch(Exception ex)
            {
                _serverConnection?.Dispose();
                _logger?.Exception(ex, "Failed to initialize " + nameof(MainProcessor));
                throw;
            }


        }
        public async Task StartServer()
        {
            try
            {
                using (_serverConnection)
                {
                    _serverConnection.OpenConnection();
                    using (var clientContext = _awaiter.WaitingForClient())
                    {
                        await InternalCycle(clientContext);
                    }
                }
            }
            catch (Exception ex) 
            {
                _logger.Exception(ex, "Fail within method " + nameof(StartServer));
                throw;
            }
        }

        private async Task InternalCycle(IClientContextHolder context)
        {
            try
            {
                var request = await context.Messenger.ReceiveMessageAsync();
                Boolean correctServiceCommand = _serviceCommandProcessor.TryGetLibVersionCommand(request);

                if (!correctServiceCommand)
                {
                    _logger.Error(String.Format("Wrong command provided: [{0}]", request));
                    return;
                }

                String responseWithVersion = _serviceCommandBuider.BuildReturnVersionCommand();
                await context.Messenger.SendMessageAsync(responseWithVersion);

                while (true)
                {
                    var command = await context.Messenger.ReceiveMessageAsync();
                    var t = _commandProcessor.HandleRequest(command);
                    _logger.Info(String.Format("Command [{0}] was requested", command));
                    
                    String response = await t;
                    await context.Messenger.SendMessageAsync(response);
                }
            }
            catch(Exception ex) 
            {
                _logger.Exception(ex, "Fail within internal cycle");
                throw;
            }
        }
    }
}
