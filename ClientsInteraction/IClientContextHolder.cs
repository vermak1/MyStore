using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal interface IClientContextHolder : IDisposable
    {
        IMessenger Messenger { get; }
    }
}
