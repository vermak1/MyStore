using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using MyStore.Server.Database;

namespace MyStore.Server
{
    internal class SQLDbAccessor : IDbAccessor
    {
        private static IDbAccessor _instance;

        private static readonly Object sync = new Object();
        private SQLDbAccessor() { }
        public static IDbAccessor Instance
        {
            get
            {
                if (_instance != null) 
                    return _instance;

                lock(sync)
                {
                    if (_instance == null)
                    {
                        Log.Info("Sql configuration is selected");
                        _instance = new SQLDbAccessor();
                    }
                    return _instance;
                }
            }
        }
        private async Task<DataSet> ExecuteStoredProcedureReadAsyncInternal(string spName, IEnumerable<SqlParameter> spParams)
        {
            if (spParams == null)
                throw new ArgumentNullException(nameof(spParams));
            if (String.IsNullOrEmpty(spName))
                throw new ArgumentException(nameof(spName));

            DataSet ds = new DataSet();
            using (var connection = await SQLConnectionsFactory.GetConnectionAsync())
            {
                SqlCommand command = new SqlCommand(spName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var p in spParams)
                    command.Parameters.AddWithValue(p.ParameterName, p.Value);
                try
                {
                    await command.ExecuteNonQueryAsync();
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                }

                catch (Exception ex)
                {
                    Log.Exception(ex, "Stored procedure execution has been failed: stored procedure: {0}", spName);
                    throw;
                }
                return ds;
            }
        }

        private async Task<DataSet> ExecuteStoredProcedureReadAsyncInternal(string spName)
        {
            if (String.IsNullOrEmpty(spName))
                throw new ArgumentException(nameof(spName));

            DataSet ds = new DataSet();
            using (var connection = await SQLConnectionsFactory.GetConnectionAsync())
            {
                SqlCommand command = new SqlCommand(spName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    await command.ExecuteNonQueryAsync();
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                }

                catch (Exception ex)
                {
                    Log.Exception(ex, "Stored procedure execution has been failed: stored procedure: {0}", spName);
                    throw;
                }
                return ds;
            }
        }

        private async Task<Int32> ExecuteStoredProcedureWriteAsyncInternal(string spName, IEnumerable<SqlParameter> spParams)
        {
            if (spParams == null)
                throw new ArgumentNullException(nameof(spParams));
            if (String.IsNullOrEmpty(spName))
                throw new ArgumentException(nameof(spName));

            Int32 rowsAffected = 0;
            using (var connection = await SQLConnectionsFactory.GetConnectionAsync())
            {
                SqlCommand command = new SqlCommand(spName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                foreach (var p in spParams)
                    command.Parameters.AddWithValue(p.ParameterName, p.Value);
                try
                {
                    rowsAffected = await command.ExecuteNonQueryAsync();
                }

                catch (Exception ex)
                {
                    Log.Exception(ex, "Stored procedure execution has been failed: stored procedure: {0}", spName);
                    throw;
                }

                return rowsAffected;
            }
        }

        public async Task<DataSet> RunStoredProcedureReadAsync(String procedureName, IEnumerable<SqlParameter> spParams)
        {           
            return await ExecuteStoredProcedureReadAsyncInternal(procedureName, spParams);
        }

        public async Task<Int32> RunStoredProcedureWriteAsync(String procedureName, IEnumerable<SqlParameter> spParams)
        {
            return await ExecuteStoredProcedureWriteAsyncInternal(procedureName, spParams);
        }

        public async Task<DataSet> RunStoredProcedureReadAsync(string spName)
        {
            return await ExecuteStoredProcedureReadAsyncInternal(spName);
        }

        public async Task<DataSet> RunStoredProcedureReadAsync(string spName, SqlParameter parameter)
        {
            IEnumerable<SqlParameter> parameters = new SqlParameter[] { parameter };
            return await ExecuteStoredProcedureReadAsyncInternal(spName, parameters);
        }
    }
}
