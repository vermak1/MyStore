using System;

namespace MyStore.Server
{
    internal interface IConnectionConfigurator
    {
        IClientAwaiter GetClientAwaiter();
    }
}
