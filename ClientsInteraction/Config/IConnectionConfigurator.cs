using System;

namespace MyStore.Server
{
    internal interface IConnectionConfigurator : IDisposable
    {
        IServerConnectionHolder GetConnectionHolder();

        IClientAwaiter GetClientAwaiter();
    }
}
