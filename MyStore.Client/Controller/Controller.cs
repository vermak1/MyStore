using System;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class Controller : IController
    {
        private readonly ILogger _logger;

        private readonly IServerInteractor _serverInteractor;

        public Controller()
        {
            _logger = Configurator.Instance.GetLogger();
            _serverInteractor = new ServerInteractor();
        }

        public void Dispose() 
        {
            _serverInteractor?.Dispose();
        }

        public async Task<IResult> GetAllCarsCommand(UserListAllCarsCommand c)
        {
            try
            {
                ListCarsResponseInfo response = await _serverInteractor.GetListCars();
                return ResultFactory.CarListResult(EResultStatus.Success, response);
            }
            catch(Exception ex)
            {
                return ResultFactory.Error(String.Format("Couldn't get result\nError: {0}", ex.Message));
            }
        }
    }
}
