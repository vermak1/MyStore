using System.Collections.Generic;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Server
{
    internal class CarRepository : ICarRepository
    {
        private readonly IDbAccessor _dbAccessor;
        public CarRepository()
        {
            _dbAccessor = DbAccessorFactory.GetDbAccessor();
        }

        public async Task<List<CarInfo>> ListCarsAsync()
        {
            var dataSet = await _dbAccessor.RunStoredProcedureReadAsync("ListAllCars");
            return DbDataConverter.ConvertToCarInfo(dataSet);
        }
    }
}
