using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MyStore.Server.Database
{
    internal class TransactionWrapper
    {
        public static async Task<Boolean> ExecuteInTransactionAsync(Func<Task> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            using(var connection = await SQLConnectionsFactory.GetConnectionAsync())
            {
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    await func();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Transaction has been failed\nError: {0} \nTry to rollback transaction..", ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
