using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal interface IClientAwaiter
    {
        Task<IClientProcessor> WaitAndProcessClient(CancellationToken token);
    }
}
