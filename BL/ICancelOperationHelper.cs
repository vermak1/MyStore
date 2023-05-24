using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal interface ICancelOperationHelper : IDisposable
    {
        void WaitForCompletedOperations();
        void SignalStop();
        void AddTask(Task<IClientProcessor> task);
        CancellationToken ExitRequestedToken { get; }
    }
}
