using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal interface IMessenger
    {
        Task<String> ReceiveMessageAsync();

        Task SendMessageAsync(String message);
    }
}
