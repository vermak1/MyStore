using System;

namespace MyStore.Server
{
    internal interface IClientAwaiter
    {
        IClientContextHolder WaitingForClient();
    }
}
