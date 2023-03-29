using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Server.Database
{
    internal class SQLCustomer : ICustomerRepository
    {
        private readonly SQLStoredProcedureExecutor _executor;

        public SQLCustomer()
        {
            _executor = new SQLStoredProcedureExecutor();
        }
        public async Task<CustomerInfo> FindCustomerByIdAsync(Guid id)
        {
            using (var connection = await SQLConnectionsFactory.GetConnectionAsync())
            {
                Dictionary<String, String> queryParams = new Dictionary<String, String>()
                {
                    ["@id"] = id.ToString()
                };
                DataSet data = await _executor.RunStoredProcedureReadAsync("FindCustomerById", queryParams, connection);
                var userInfo = ConvertToCustomerInfo(data);
                if (userInfo == null)
                    Console.WriteLine("There is no user with id {0}", id);
                return userInfo;
            }
        }

        public async Task<CustomerInfo> FindCustomerByNameAsync(String firstName, String lastName)
        {
            if (String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName))
                throw new ArgumentException("first name or last name is null or empty");

            using (var connection = await SQLConnectionsFactory.GetConnectionAsync()) 
            {
                Dictionary<String, String> queryParams = new Dictionary<String, String>()
                {
                    ["@firstname"] = firstName,
                    ["@lastname"] = lastName
                };
                DataSet data = await _executor.RunStoredProcedureReadAsync("FindCustomerByName", queryParams, connection);
                var userInfo = ConvertToCustomerInfo(data);
                if (userInfo == null)
                    Console.WriteLine("There is no user with first name {0} and last name {1}", firstName, lastName);
                return userInfo;
            }
        }

        public async Task<Int32> CreateCustomerAsync(CustomerInfo customerInfo)
        {
            if (customerInfo == null)
                throw new ArgumentNullException(nameof(customerInfo));

            using (var connection = await SQLConnectionsFactory.GetConnectionAsync())
            {
                Dictionary<String, String> queryParams = new Dictionary<String, String>()
                {
                    ["@id"] = Guid.NewGuid().ToString(),
                    ["@firstname"] = customerInfo.FirstName,
                    ["@lastname"] = customerInfo.LastName,
                    ["@createddate"] = DateTime.Now.ToString(),
                    ["@address"] = customerInfo.Address,
                    ["@city"] = customerInfo.City,
                    ["@number"] = customerInfo.Number,
                    ["@mail"] = customerInfo.Mail
                };
                Int32 rows = -1;
                try
                {
                    rows = await _executor.RunStoredProcedureWriteAsync("CreateCustomer", queryParams, connection);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Error during customer creation.\nError: {0}", ex.Message);
                    throw;
                }
                
                if (rows == 1)
                    Console.WriteLine("Customer created successfully");
                else
                    Console.WriteLine("Customer hasn't been created");
                return rows;
            }
        }

        private CustomerInfo ConvertToCustomerInfo(DataSet data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Tables[0].Rows.Count == 0)
            {
                Console.WriteLine("There is not user returned from DB");
                return null;
            }
            return new CustomerInfo()
            {
                Id = Guid.Parse(data.Tables[0].Rows[0]["id"].ToString()),
                FirstName = data.Tables[0].Rows[0]["first_name"].ToString(),
                LastName = data.Tables[0].Rows[0]["last_name"].ToString(),
                Address = data.Tables[0].Rows[0]["address"].ToString(),
                City = data.Tables[0].Rows[0]["city"].ToString(),
                Mail = data.Tables[0].Rows[0]["mail"].ToString(),
                Number = data.Tables[0].Rows[0]["number"].ToString(),
            };
        }

    }
}
