using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal interface IMessenger
    {
        Task<String> ReceiveMessageAsync();

        Task SendMessageAsync(string message);

        Boolean IsConnected { get; }

        String Address { get; }
    }
}
