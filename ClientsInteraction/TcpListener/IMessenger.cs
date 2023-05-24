using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal interface IMessenger : IDisposable
    {
        Task<String> ReceiveMessageAsync();

        Task SendMessageAsync(string message);
    }
}
