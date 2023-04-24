using System;
using System.Threading;

namespace MyStore.Server
{
    internal class MainProcessor
    {
        public void StartServer()
        {
            try
            {
                ConnectionProvider connectionProvider = new ConnectionProvider();
                IClientAwaiter clientAwaiter = connectionProvider.GetClientAwaiter();
                    
                using (AutoResetEvent wh = new AutoResetEvent(false))
                {
                    ThreadPool.GetAvailableThreads(out Int32 availableThreads, out Int32 availableIOThreads);
                    while (availableThreads > 0 && availableIOThreads > 0)
                    {
                        ThreadPool.QueueUserWorkItem(async (o) =>
                        {
                            await clientAwaiter.WaitingAndProcessClientAsync(wh);
                        });
                        wh.WaitOne();
                        ThreadPool.GetAvailableThreads(out availableThreads, out availableIOThreads);
                        Log.Info("Available threads: {0}, available IO threads: {1}", availableThreads, availableIOThreads);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Fail within method {0}", nameof(StartServer));
                throw;
            }
        }
    }
}
