using System;
using MyStore.CommonLib;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class ListCarsCommandHandler
    {
        private readonly ICarRepository _carRepository;

        private readonly ResponseFactory _factory;

        public ListCarsCommandHandler()
        {
            _factory = new ResponseFactory();
            _carRepository = new CarRepository();
        }
        public async Task<String> GenerateResponse()
        {
            var t = _carRepository.ListCarsAsync();
            ListCarsResponseInfo info = new ListCarsResponseInfo()
            {
                CarInfos = await t,
                Type = ECommandType.ListAllCars
            };
            
            return _factory.ResponseListAllCars(info);
        }
    }
}
