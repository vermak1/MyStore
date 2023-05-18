using MyStore.CommonLib;
using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal interface IServerInteractor : IDisposable
    {
        Task<ListCarsResponseInfo> GetListCars(UserListAllCarsCommand command);
    }
}
