using MyStore.Client.Config;
using System;

namespace MyStore.Client
{
    internal class Configurator
    {
        private readonly ConfigurationStringProvider _provider;

        private readonly ConnectionConfigurator _connectionConfig;

        private readonly LoggerConfigurator _loggerConfig;

        private readonly MessengerConfiguration _messengerConfig;

        private readonly UserInterfaceConfiguration _userInterfaceConfig;

        private readonly ILogger _logger;

        public Configurator(String path)
        {
            _provider = new ConfigurationStringProvider(path);
            _connectionConfig = new ConnectionConfigurator();
            _loggerConfig = new LoggerConfigurator();
            _messengerConfig = new MessengerConfiguration();
            _userInterfaceConfig = new UserInterfaceConfiguration();
            if (!_provider.ConfigExist)
                _logger = _loggerConfig.GetDefaultLogger();
        }

        public IConnectionHolder GetConnectHolder()
        {
            if (!_provider.ConfigExist)
                return _connectionConfig.GetDefaultConnectHolder(_logger);
            throw new NotImplementedException();
        }

        public IMessenger GetMessenger(IConnectionHolder connection)
        {
            return _messengerConfig.GetMessenger(connection, _logger);
        }

        public ILogger GetLogger()
        {
            if (!_provider.ConfigExist)
                return _loggerConfig.GetDefaultLogger();
            throw new NotImplementedException();
        }

        public IUserInterface GetUserInterface()
        {
            if (!_provider.ConfigExist)
                return _userInterfaceConfig.GetDefaultUserInterface();

            throw new NotImplementedException();
        }
    }
}
