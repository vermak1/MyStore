using System;
using MyStore.CommonLib;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class ListCarsCommandHandler
    {
        private readonly Car _cars;

        private readonly ResponseFactory _factory;

        public ListCarsCommandHandler()
        {
            _factory = new ResponseFactory();
            _cars = Car.CreateForSql();
        }
        public async Task<String> GenerateResponse()
        {
            var t = _cars.ListCarsAsync();
            ListCarsResponseInfo info = new ListCarsResponseInfo()
            {
                CarInfos = await t,
                Type = ECommandType.ListAllCars
            };

            String result = _factory.ResponseListAllCars(info);
            return result;
        }
    }
}
