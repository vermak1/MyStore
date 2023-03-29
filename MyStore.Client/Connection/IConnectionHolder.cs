using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal interface IConnectionHolder : IDisposable
    {
        Task<Boolean> ConnectToServerAsync();
        void DisconnectFromServer();
    }
}
