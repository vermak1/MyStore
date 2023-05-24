using System;
using System.Net.Sockets;

namespace MyStore.Client
{
    internal interface ISocketSettingsProvider
    {
        String IP { get; }

        Int32 Port { get; }

        AddressFamily AddressFamily { get; }

        SocketType SocketType { get; }

        ProtocolType ProtocolType { get; }
    }
}
