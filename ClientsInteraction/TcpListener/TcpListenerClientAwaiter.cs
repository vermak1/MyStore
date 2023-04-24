using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class TcpListenerClientAwaiter : IClientAwaiter
    {
        private readonly TcpListener _tcpListener;
        public TcpListenerClientAwaiter(IPEndPoint endpoint)
        {
            _tcpListener = new TcpListener(endpoint);
            StartServer();
        }

        private void StopServer()
        {
            _tcpListener.Stop();
            Log.Info("Tcp listener has been stopped");
        }

        private void StartServer()
        {
            _tcpListener.Start();
            Log.Info("Start listening for incoming connections on address '{0}'", _tcpListener.LocalEndpoint);
        }

        public async Task WaitingAndProcessClientAsync(AutoResetEvent wh)
        {
            Log.Info("Waiting for a new client");
            try
            {
                using (var tcpClient = await _tcpListener.AcceptTcpClientAsync())
                {
                    wh.Set();
                    Log.Info("New client '{0}' connected", tcpClient.Client.RemoteEndPoint);
                    IClientProcessor processor = new TcpClientProcessor(tcpClient);
                    await processor.ProcessClient();
                }
            }
            catch(Exception ex)
            {
                StopServer();
                Log.Exception(ex, "Fail during communication cycle with client");
            }
        }
    }
}
