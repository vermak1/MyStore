using System.Data.SqlClient;

namespace MyStore.Server
{
    internal static class SParameterFactory
    {
        public static SqlParameter MakeParam(string name, object value)
        {
            var param = new SqlParameter
            {
                ParameterName = name,
                Value = value,
            };
            return param;
        }
    }
}
