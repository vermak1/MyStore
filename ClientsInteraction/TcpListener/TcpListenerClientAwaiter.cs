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

        private Task<IClientProcessor> ProcessNewCustomer(TcpClient tcpClient, CancellationToken token)
        {
            Task<IClientProcessor> task = Task.Run(async () =>
            {
                using (IClientProcessor clientProcessor = new TcpClientProcessor(tcpClient, token))
                {
                    try
                    {
                        await clientProcessor.ProcessClient();
                    }
                    catch (OperationCanceledException)
                    { }
                    catch (Exception ex)
                    {
                        Log.Exception(ex, "Failed to process client");
                    }
                    return clientProcessor;
                }
            });
            return task;
        }

        public Task<IClientProcessor> WaitAndProcessClient(CancellationToken token)
        {
            Log.Info("Waiting for a new client");
            TcpClient tcpClient = null;
            using (CancellationTokenRegistration tokenRegistration = token.Register(StopAwaitClients))
            {
                try
                {
                    var task = Task.Run(async () =>
                    {
                        return await _tcpListener.AcceptTcpClientAsync();
                    });
                    tcpClient = task.Result;
                    Log.Info("Client {0} connected to server", tcpClient.Client.RemoteEndPoint);
                }
                catch
                {
                    tcpClient?.Dispose();
                    token.ThrowIfCancellationRequested();
                    throw;
                }
            }
            return ProcessNewCustomer(tcpClient, token);
        }
    }
}
