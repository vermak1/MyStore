using System;
using System.Net.Sockets;

namespace MyStore.Server
{
    internal interface IServerSocketProvider
    {
        Socket ConfigureServerSocket();
    }
}
