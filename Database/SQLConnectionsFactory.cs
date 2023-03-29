using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;

namespace MyStore.Server.Database
{
    internal class SQLConnectionsFactory
    {
        public static async Task<SqlConnection> GetConnectionAsync()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["shop1"].ConnectionString);
            await conn.OpenAsync();
            return conn;
        }
    }
}
