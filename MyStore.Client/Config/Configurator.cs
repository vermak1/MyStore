using System;

namespace MyStore.Client
{
    internal class Configurator
    {
        private readonly ConfigurationStringProvider _provider;

        private readonly LoggerConfigurator _loggerConfig;

        private readonly MessengerConfiguration _messengerConfig;

        private readonly UserInterfaceConfiguration _userInterfaceConfig;

        private readonly ILogger _logger;

        public Configurator(String path)
        {
            _loggerConfig = new LoggerConfigurator();
            _provider = new ConfigurationStringProvider(path);
            if (!_provider.ConfigExist)
                _logger = _loggerConfig.GetDefaultLogger();

            _messengerConfig = new MessengerConfiguration();
            _userInterfaceConfig = new UserInterfaceConfiguration();
        }

        public IMessenger GetMessenger()
        {
            if (!_provider.ConfigExist)
                return _messengerConfig.GetDefaultMessenger(_logger);

            throw new NotImplementedException();
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
