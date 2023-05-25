using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class MainProcessor : IDisposable
    {
        private readonly IClientAwaiter _clientAwaiter;
        private readonly ICancelApplicationHelper _cancelApplicationHelper;
        public MainProcessor()
        {
            Console.CancelKeyPress += OnCancelKeyPress;
            _cancelApplicationHelper = new CancelApplicationHelper();
            _clientAwaiter = ClientAwaiterFactory.GetClientAwaiter();
        }
        public void Dispose()
        {
            _cancelApplicationHelper?.Dispose();
        }

        private void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Log.Info("Application cancellation has been requested");
            e.Cancel = true;
            _cancelApplicationHelper.SignalStop();
        }

        public void StartServer()
        {
            Log.Info("Server is started");
            while (!_cancelApplicationHelper.ExitRequestedToken.IsCancellationRequested)
            {
                try
                {
                    var task = _clientAwaiter.WaitAndProcessClient(_cancelApplicationHelper.ExitRequestedToken);
                    if (task.Status != TaskStatus.Canceled)
                        _cancelApplicationHelper.AddTask(task);
                }
                catch (OperationCanceledException)
                { }
                catch (Exception ex)
                {
                    Log.Exception(ex, "Failed to process new client");
                }
            }
            _cancelApplicationHelper.WaitForCompletedOperations();
        }

    }
}
