using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class MainProcessor : IDisposable
    {
        private readonly IClientAwaiter _clientAwaiter;
        private readonly ICancelOperationHelper _clientsContext;
        public MainProcessor()
        {
            Console.CancelKeyPress += OnCancelKeyPress;
            _clientsContext = new CancelOperationHelper();
            _clientAwaiter = ClientAwaiterFactory.GetClientAwaiter();
        }
        public void Dispose()
        {
            _clientsContext?.Dispose();
        }

        private void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Log.Info("Application cancellation has been requested");
            e.Cancel = true;
            _clientsContext.SignalStop();
        }

        public void StartServer()
        {
            Log.Info("Server is started");
            while (!_clientsContext.ExitRequestedToken.IsCancellationRequested)
            {
                try
                {
                    var task = _clientAwaiter.WaitAndProcessClient(_clientsContext.ExitRequestedToken);
                    if (task.Status != TaskStatus.Canceled)
                        _clientsContext.AddTask(task);
                }
                catch (OperationCanceledException)
                { }
                catch (Exception ex)
                {
                    Log.Exception(ex, "Failed to process new client");
                }
            }
            _clientsContext.WaitForCompletedOperations();
        }

    }
}
