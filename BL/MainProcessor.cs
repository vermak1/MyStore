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

        public async Task StartServer()
        {
            Log.Info("Server is started");
            while (!_cancelApplicationHelper.ExitRequestedToken.IsCancellationRequested)
            {
                try
                {
                    await _clientAwaiter.WaitAndProcessClient(_cancelApplicationHelper.ExitRequestedToken, _cancelApplicationHelper);
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
