using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class CancelOperationHelper : ICancelOperationHelper
    {
        private readonly List<Task<IClientProcessor>> _clients;
        private readonly CancellationTokenSource _cancellationTokenSource;
        public CancelOperationHelper()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _clients = new List<Task<IClientProcessor>>();
        }

        public CancellationToken ExitRequestedToken => _cancellationTokenSource.Token;

        public void AddTask(Task<IClientProcessor> task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            _clients.Add(task);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }

        public void SignalStop()
        {
            _cancellationTokenSource.Cancel();
        }

        public void WaitForCompletedOperations()
        {
            Task.WhenAll(_clients.ToArray()).Wait();
        }
    }
}
