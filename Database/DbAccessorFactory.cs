using System;
using System.Configuration;

namespace MyStore.Server
{
    internal class DbAccessorFactory
    {
        public static IDbAccessor GetDbAccessor()
        {
            var config = ConfigurationManager.ConnectionStrings["SQL"];
            if (config == null)
            {
                Log.Error("Configuration with name 'SQL' is not found");
                throw new Exception("Configuration with name 'SQL' is not found");
            }
            return SQLDbAccessor.Instance;
        }
    }
}
