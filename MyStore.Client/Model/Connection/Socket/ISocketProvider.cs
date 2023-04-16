using System;
using System.Net.Sockets;

namespace MyStore.Client
{
    internal interface ISocketProvider
    {
        Socket ConfigureServerSocket();
    }
}
