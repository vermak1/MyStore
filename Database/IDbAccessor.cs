using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MyStore.Server
{
    internal interface IDbAccessor
    {
        Task<DataSet> RunStoredProcedureReadAsync(String spName, IEnumerable<SqlParameter> parameters);
        Task<DataSet> RunStoredProcedureReadAsync(String spName);
        Task<Int32> RunStoredProcedureWriteAsync(String procedureName, IEnumerable<SqlParameter> spParams);
    }
}
