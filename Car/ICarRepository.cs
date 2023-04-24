using System.Threading.Tasks;
using System.Collections.Generic;
using MyStore.CommonLib;

namespace MyStore.Server
{
    internal interface ICarRepository
    {
        Task<List<CarInfo>> ListCarsAsync();
    }
}
