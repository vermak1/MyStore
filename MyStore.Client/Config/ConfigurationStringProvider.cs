using System;
using System.IO;

namespace MyStore.Client
{
    internal class ConfigurationStringProvider
    {
        private readonly String _path;

        public Boolean ConfigExist { get; }

        public ConfigurationStringProvider()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length == 1)
            {
                ConfigExist = false;
                return;
            }
            _path = args[1];
            ConfigExist = TryGetConfiguration();
        }

        private Boolean TryGetConfiguration()
        {
            if (File.Exists(_path))
                return true;
            return false;
        }

        public String GetConfiguration()
        {
            return File.ReadAllText(_path);
        }
    }
}
