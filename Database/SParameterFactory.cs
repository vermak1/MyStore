using System.Data.SqlClient;

namespace MyStore.Server.Database
{
    internal static class SParameterFactory
    {
        public static SqlParameter MakeParam(string name, string value)
        {
            var param = new SqlParameter
            {
                ParameterName = name,
                Value = value
            };
            return param;
        }
    }
}
