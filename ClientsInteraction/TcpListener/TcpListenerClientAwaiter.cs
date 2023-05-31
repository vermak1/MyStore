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
            StartAwaitClients();
        }

        private void StopAwaitClients()
        {
            _tcpListener.Stop();
            Log.Info("Tcp listener has been stopped receiving new connections");
        }

        private void StartAwaitClients()
        {
            _tcpListener.Start();
            Log.Info("Start listening for incoming connections on address '{0}'", _tcpListener.LocalEndpoint);
        }

        private void ProcessNewCustomer(TcpClient tcpClient, CancellationToken token, ICancelApplicationHelper helper)
        {
            ThreadPool.QueueUserWorkItem(async (o) =>
            {
                IClientProcessor clientProcessor = new TcpClientProcessor(tcpClient, token, helper);
                try
                {
                    await clientProcessor.ProcessClient();
                }
                catch (Exception e)
                {
                    Log.Exception(e, "Failed to process client");
                }
                finally
                {
                    clientProcessor?.Dispose();
                    tcpClient.Dispose();
                }
            });
        }

        public async Task WaitAndProcessClient(CancellationToken token, ICancelApplicationHelper helper)
        {
            Log.Info("Waiting for a new client");
            TcpClient tcpClient = null;
            using (CancellationTokenRegistration tokenRegistration = token.Register(StopAwaitClients))
            {
                try
                {
                    tcpClient = await _tcpListener.AcceptTcpClientAsync();
                    Log.Info("Client {0} connected to server", tcpClient.Client.RemoteEndPoint);
                    ProcessNewCustomer(tcpClient, token, helper);
                }
                catch
                {
                    tcpClient?.Dispose();
                    token.ThrowIfCancellationRequested();
                    throw;
                }
            }
            
        }
    }
}
