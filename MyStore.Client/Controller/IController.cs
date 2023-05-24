using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal interface IController : IDisposable
    {
        Task<IResult> GetAllCarsCommand(UserListAllCarsCommand command);
    }
}
