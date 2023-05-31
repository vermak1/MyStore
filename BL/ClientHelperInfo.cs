using System;
using System.Threading;

namespace MyStore.Server
{
    internal class ClientHelperInfo : IDisposable
    {
        public String Address { get; }

        public ManualResetEvent WorkIsEndEvent { get; }

        public ClientHelperInfo(String address)
        {
            Address = String.IsNullOrEmpty(address) ? throw new ArgumentException(nameof(address)) : address;
            WorkIsEndEvent = new ManualResetEvent(false);
        }
        public void Dispose()
        {
            WorkIsEndEvent?.Dispose();
        }
    }
}
