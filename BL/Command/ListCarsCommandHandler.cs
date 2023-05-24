using System;
using MyStore.CommonLib;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        public async Task<String> GetCars(ListCarCommand command)
        {
            List<CarContainer> result;
            switch (command.CommandType)
            {
                case ECommandType.ListAllCars:
                    result = await _carRepository.ListAllCarsAsync();
                    break;
                case ECommandType.ListAllCarsByName:
                    result = await _carRepository.ListCarsByNameAsync(command.Model);
                    break;
                case ECommandType.ListAllCarsByYear:
                    result = await _carRepository.ListCarsByYearAsync(command.Year);
                    break;
                case ECommandType.ListAllCarsByNameAndYear:
                    result = await _carRepository.ListCarsByNameAndYearAsync(command.Model, command.Year);
                    break;
                default:
                    throw new Exception("There is not such command type for retrieving cars");
            }
            return GetStringFromResult(result);
        }

        private String GetStringFromResult(List<CarContainer> cars)
        {
            var commonLibInfo = ContainerConverter.ConvertFromServerListToCommonLib(cars);
            return _factory.ResponseListAllCars(commonLibInfo);
        }
    }
}
