using System;
using System.Net.Sockets;


namespace MyStore.Server
{
    internal class SocketSettingsHardcodeProvider : IServerSocketSettingsProvider
    {
        public Int32 Port => 6667;

        public AddressFamily AddressFamily => AddressFamily.InterNetwork;

        public SocketType SocketType => SocketType.Stream;

        public ProtocolType ProtocolType => ProtocolType.Tcp;

        public String IP => "127.0.0.1";

        public Int32 MaxConnections => 500;
    }
}
