using System;
using System.Threading.Tasks;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class Controller : IController
    {
        private readonly IServerInteractor _serverInteractor;

        private readonly ServerCommandsConstructor _commandsConstructor;

        private readonly ResponseConverter _responseConverter;

        public Controller()
        {
            _serverInteractor = new ServerInteractor();
            _commandsConstructor = new ServerCommandsConstructor();
            _responseConverter = new ResponseConverter();
        }

        public void Dispose() 
        {
            _serverInteractor?.Dispose();
        }

        public async Task<IResult> GetAllCarsCommand(UserListAllCarsCommand command)
        {
            try
            {
                String serialized = _commandsConstructor.GetServerCommandForListAllCars(command);
                String response = await _serverInteractor.SendCommandAndReceiveResponse(serialized);
                ListCarsResponseInfo responseInfo = _responseConverter.ConvertStringToListCars(response);
                return ResultFactory.CarListResult(EResultStatus.Success, responseInfo);
            }
            catch(Exception ex)
            {
                return ResultFactory.Error(String.Format("Couldn't get result\nError: {0}", ex.Message));
            }
        }
    }
}
