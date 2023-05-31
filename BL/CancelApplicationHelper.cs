using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MyStore.Server
{
    internal class CancelApplicationHelper : ICancelApplicationHelper
    {
        private readonly List<ClientHelperInfo> _clients;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly Object _sync = new Object();
        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(10);
        private readonly TimeSpan _cleanupFrequency = TimeSpan.FromSeconds(5);
        public CancelApplicationHelper()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _clients = new List<ClientHelperInfo>();
            RunCleanupThread();
        }

        public CancellationToken ExitRequestedToken => _cancellationTokenSource.Token;

        public ClientHelperInfo AddClient(ClientHelperInfo client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            lock(_sync)
                _clients.Add(client);
            return client;
        }

        public void Dispose()
        {
            foreach (ClientHelperInfo client in _clients)
                client.Dispose();

            _cancellationTokenSource?.Dispose();
        }

        private void RemoveAndDisposeClient(ClientHelperInfo client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            lock(_sync)
            {
                client.Dispose();
                _clients.Remove(client);
                Log.Info("Client {0} was removed from list", client.Address);
            }
        }

        public void SignalStop()
        {
            _cancellationTokenSource.Cancel();
        }

        public void WaitForCompletedOperations()
        {
            Stopwatch sw = Stopwatch.StartNew();
            while(sw.Elapsed < _timeout)
            {
                foreach (var client in _clients.ToArray())
                {
                    if (client.WorkIsEndEvent.WaitOne(0))
                        RemoveAndDisposeClient(client);
                }
                if (_clients.Count == 0)
                    return;
                Thread.Sleep(1000);
            }
            CheckForAllClientsProcessed();
        }

        private void CheckForAllClientsProcessed()
        {
            if (_clients.Count > 0)
            {
                Log.Error("Timeout '{0}' before application cancellation exceeded", _timeout);
                String message = String.Format("Not all clients were successfully processed before application is closed, clients: {0}",
                    _clients.Select(c => c.Address).Aggregate((prev, next) => prev + ", " + next));
                throw new Exception(message);
            }
        }

        private void RunCleanupThread()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    Log.Info("Client cleanup thread started");
                    while(!ExitRequestedToken.IsCancellationRequested)
                    {
                        foreach(var client in _clients.ToArray())
                        {
                            if (client.WorkIsEndEvent.WaitOne(0))
                                RemoveAndDisposeClient(client);
                        }
                        Thread.Sleep(_cleanupFrequency);
                    }
                    Log.Info("Client cleanup thread finished");
                }
                catch (Exception ex)
                {
                    Log.Exception(ex, "Client cleanup thread failed");
                    RunCleanupThread();
                }
            });
        }
    }
}
