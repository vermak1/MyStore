using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal interface ICancelApplicationHelper : IDisposable
    {
        void WaitForCompletedOperations();
        void SignalStop();
        void AddTask(Task<IClientProcessor> task);
        CancellationToken ExitRequestedToken { get; }
    }
}
