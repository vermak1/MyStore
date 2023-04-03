using System;

namespace MyStore.Server
{
    internal class ConnectionProvider : IDisposable
    {
        private readonly IConnectionConfigurator _connectionConfigurator;

        public ConnectionProvider(ILogger logger)
        {
            _connectionConfigurator = new SocketConnectionConfigurator(logger);
        }

        public IServerConnectionHolder GetConnectionHolder()
        {
            return GetDefaultConnectionHolder();
        }

        public IClientAwaiter GetClientAwaiter()
        {
            return GetDefaultClientAwaiter();
        }

        private IServerConnectionHolder GetDefaultConnectionHolder()
        {
            return _connectionConfigurator.GetConnectionHolder();
        }

        private IClientAwaiter GetDefaultClientAwaiter()
        {
            return _connectionConfigurator.GetClientAwaiter();
        }

        public void Dispose()
        {
            _connectionConfigurator?.Dispose();
        }
    }
}
