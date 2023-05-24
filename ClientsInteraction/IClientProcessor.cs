using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal interface IClientProcessor : IDisposable
    {
        Task ProcessClient();
    }
}
