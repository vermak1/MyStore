using System;
using System.IO;

namespace MyStore.Client
{
    internal class ConfigurationStringProvider
    {
        private readonly String _path;

        public Boolean ConfigExist { get; }

        public ConfigurationStringProvider(String path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            _path = path;
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
