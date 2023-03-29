using System;

namespace MyStore.Client.Config
{
    internal class MessengerConfiguration
    {
        public IMessenger GetMessenger(IConnectionHolder connection, ILogger logger)
        {
            if (connection == null) 
                throw new ArgumentNullException(nameof(connection));

            if (connection is SocketConnection socketConnection)
                return new SocketMessenger(socketConnection, logger);

            throw new NotImplementedException();
        }
    }
}
