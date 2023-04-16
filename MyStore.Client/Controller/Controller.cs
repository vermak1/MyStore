using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class Controller : IController
    {
        private readonly ILogger _logger;

        private readonly ViewDTOCreator _dtoCreator;

        private readonly IServerInteractor _serverInteractor;

        public Controller()
        {
            _logger = Configurator.Instance.GetLogger();
            _dtoCreator = new ViewDTOCreator();
            _serverInteractor = new ServerInteractor();
        }

        public void Dispose() 
        {
            _serverInteractor?.Dispose();
        }

        public async Task<ViewCarsContainer> GetAllCarsCommand(UserListAllCarsCommand c)
        {
            _logger.Info("Got command [{0}] from user", c.CommandType);
            var response = await _serverInteractor.GetListCars();
            return _dtoCreator.ConvertCarList(response);
        }
    }
}
