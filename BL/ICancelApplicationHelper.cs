using System;
using System.Threading;

namespace MyStore.Server
{
    internal interface ICancelApplicationHelper : IDisposable
    {
        void WaitForCompletedOperations();
        void SignalStop();
        ClientHelperInfo AddClient(ClientHelperInfo client);
        CancellationToken ExitRequestedToken { get; }
    }
}
