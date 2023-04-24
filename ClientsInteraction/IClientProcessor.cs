using System.Threading.Tasks;

namespace MyStore.Server
{
    internal interface IClientProcessor
    {
        Task ProcessClient();
    }
}
