using System;

namespace MyStore.Client
{
    internal class Configurator
    {
        private static object syncRoot = new Object();

        private static Configurator _instance;

        public static Configurator Instance
        { get
            {
                if (_instance != null)
                    return _instance;
                lock (syncRoot) 
                {
                    if (_instance == null)
                        _instance = new Configurator();
                    return _instance;
                }
            }
        }
        private readonly ConfigurationStringProvider _provider;

        private readonly ILogger _logger;

        private Configurator()
        {
            _provider = new ConfigurationStringProvider();
            if (!_provider.ConfigExist)
                _logger = LoggerFactory.GetDefaultLogger();
        }

        public IMessenger GetMessenger()
        {
            if (!_provider.ConfigExist)
                return MessengerFactory.GetTcpClientMessenger();

            throw new NotImplementedException();
        }

        public ILogger GetLogger()
        {
            if (_logger != null)
                return _logger;
            if (!_provider.ConfigExist)
                return LoggerFactory.GetDefaultLogger();

            throw new NotImplementedException();
        }
    }
}
