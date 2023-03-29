using System;
using System.Net.Sockets;

namespace MyStore.Server
{
    internal interface IServerSocketSettingsProvider
    {
        String IP { get; }

        Int32 Port { get; }
        AddressFamily AddressFamily { get; }

        SocketType SocketType { get; }

        ProtocolType ProtocolType { get; }

        Int32 MaxConnections { get; }
    }
}
