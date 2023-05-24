using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class CarRepository : ICarRepository
    {
        private readonly IDbAccessor _dbAccessor;
        public CarRepository()
        {
            _dbAccessor = DbAccessorFactory.GetDbAccessor();
        }

        public async Task<List<CarContainer>> ListAllCarsAsync()
        {
            var dataSet = await _dbAccessor.RunStoredProcedureReadAsync("ListAllCars");
            return DbDataConverter.ConvertToListCarContainer(dataSet);
        }

        public async Task<List<CarContainer>> ListCarsByNameAsync(string name)
        {
            var param1 = SParameterFactory.MakeParam("@name", name);
            var dataSet = await _dbAccessor.RunStoredProcedureReadAsync("ListCarsByName", param1);
            return DbDataConverter.ConvertToListCarContainer(dataSet);
        }

        public async Task<List<CarContainer>> ListCarsByYearAsync(Int32 year)
        {
            var param1 = SParameterFactory.MakeParam("@year", year);
            var dataSet = await _dbAccessor.RunStoredProcedureReadAsync("ListCarsByYear", param1);
            return DbDataConverter.ConvertToListCarContainer(dataSet);
        }

        public async Task<List<CarContainer>> ListCarsByNameAndYearAsync(String name, Int32 year)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                SParameterFactory.MakeParam("@year", year),
                SParameterFactory.MakeParam("@name", name)
            };

            var dataSet = await _dbAccessor.RunStoredProcedureReadAsync("ListCarsByNameAndYear", parameters);
            return DbDataConverter.ConvertToListCarContainer(dataSet);
        }
    }
}
