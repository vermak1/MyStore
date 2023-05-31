using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal interface IClientAwaiter
    {
        Task WaitAndProcessClient(CancellationToken token, ICancelApplicationHelper cancelHelper);
    }
}
