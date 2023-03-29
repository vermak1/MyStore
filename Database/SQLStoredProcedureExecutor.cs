using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace MyStore.Server.Database
{
    internal class SQLStoredProcedureExecutor
    {
        private async Task<DataSet> ExecuteStoredProcedureReadAsync(string spName, List<SqlParameter> spParams, SqlConnection connection)
        {
            DataSet ds = new DataSet();
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
                Console.WriteLine("Stored procedure execution has been failed\nError: {0}",ex.Message);
                throw;
            }
            return ds;
        }

        private async Task<Int32> ExecuteStoredProcedureWriteAsync(string spName, List<SqlParameter> spParams, SqlConnection connection)
        {
            Int32 rowsAffected = 0;
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
                Console.WriteLine("Stored procedure execution has been failed\nError: {0}",  ex.Message);
                throw;
            }

            return rowsAffected;
        }

        public async Task<DataSet> RunStoredProcedureReadAsync(String procedureName, Dictionary<String, String> spParams, SqlConnection connection)
        {
            if (spParams == null)
                throw new ArgumentNullException(nameof(spParams));
            if (String.IsNullOrEmpty(procedureName))
                throw new ArgumentException(nameof(procedureName));
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var p in spParams)
            {
                parameters.Add(new SqlParameter() { ParameterName = p.Key, Value = p.Value });
            }

            DataSet data = null;
            try
            {
                data = await ExecuteStoredProcedureReadAsync(procedureName, parameters, connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error within stored procedure {0} execution: {1}", procedureName,  ex.Message);
            }
            
            return data;
        }

        public async Task<Int32> RunStoredProcedureWriteAsync(String procedureName, Dictionary<String, String> spParams, SqlConnection connection)
        {
            if (spParams == null)
                throw new ArgumentNullException(nameof(spParams));
            if (String.IsNullOrEmpty(procedureName))
                throw new ArgumentException(nameof(procedureName));
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var p in spParams)
            {
                parameters.Add(new SqlParameter() { ParameterName = p.Key, Value = p.Value });
            }

            Int32 rowsAffected = 0;
            try
            {
                rowsAffected = await ExecuteStoredProcedureWriteAsync(procedureName, parameters, connection);
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error within stored procedure {0} execution: {1}", procedureName, ex.Message);
            }
            return rowsAffected;
        }
    }
}
