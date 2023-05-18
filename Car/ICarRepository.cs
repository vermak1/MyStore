using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace MyStore.Server
{
    internal interface ICarRepository
    {
        Task<List<CarContainer>> ListAllCarsAsync();
        Task<List<CarContainer>> ListCarsByNameAsync(String name);
        Task<List<CarContainer>> ListCarsByYearAsync(Int32 year);
        Task<List<CarContainer>> ListCarsByNameAndYearAsync(String name, Int32 year);
    }
}
