using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class ConnectionConfigurator
    {
       public IConnectionHolder GetDefaultConnectHolder(ILogger logger)
       {
            SocketSettingsHardcodeProvider settings = new SocketSettingsHardcodeProvider();
            SocketProvider socket = new SocketProvider(settings);
            RetryOptionsHardcodeProvider retries = new RetryOptionsHardcodeProvider();
            return new SocketConnection(socket, settings, logger, retries);
       }
    }
}
