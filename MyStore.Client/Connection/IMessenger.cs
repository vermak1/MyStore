using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal interface IMessenger : IDisposable
    {
        Task<Boolean> ConnectToServerAsync();
        void DisconnectFromServer();
        Task<String> SendAndReceiveMessageAsync(String message);
    }
}
