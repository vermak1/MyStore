using System;
using System.Net.Sockets;


namespace MyStore.Client
{
    internal class SocketSettingsHardcodeProvider : ISocketSettingsProvider
    {
        public Int32 Port => 6667;

        public AddressFamily AddressFamily => AddressFamily.InterNetwork;

        public SocketType SocketType => SocketType.Stream;

        public ProtocolType ProtocolType => ProtocolType.Tcp;

        string ISocketSettingsProvider.IP => "127.0.0.1";
    }
}
