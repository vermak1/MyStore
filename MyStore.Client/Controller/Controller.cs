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

        private readonly ILogger _logger;

        public Controller()
        {
            _logger = Configurator.Instance.GetLogger();
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
                String request = _commandsConstructor.GetServerCommandForListAllCars(command);
                String response = await _serverInteractor.SendCommandAndReceiveResponse(request);
                VerifyResponse(request, response);
                ListCarsResponseInfo responseInfo = _responseConverter.ConvertStringToListCars(response);
                return ResultFactory.CarListResult(EResultStatus.Success, responseInfo);
            }
            catch (Exception ex)
            {
                return ResultFactory.Error(String.Format("Couldn't get result\nError: {0}", ex.Message));
            }
        }

        private void VerifyResponse(String request, String response)
        {
            CommandInfo commandInfo = _commandsConstructor.GetTypeCommandFromString(request);
            ResponseInfo responseInfo = _responseConverter.GetResponseInfoFromResponse(response);
            VerifyResponseForErrors(responseInfo);
            VerifyTypes(commandInfo, responseInfo);
        }

        private void VerifyTypes(CommandInfo commandInfo, ResponseInfo responseInfo)
        {
            if (commandInfo.CommandType != responseInfo.Type)
            {
                _logger.Error("Type of request and response are not matched. Request type: '{0}', response type: '{1}'", commandInfo, responseInfo.Type);
                throw new Exception("The server answered incorrectly");
            }
        }

        private void VerifyResponseForErrors(ResponseInfo info)
        {
            if (info.Code == EResponseCode.ServerError)
            {
                _logger.Error("Server side error [{0}]", (info as ErrorResponseInfo)?.Message);
                throw new Exception("Error on server side occurred");
            }
            if (info.Code == EResponseCode.ClientError)
            {
                _logger.Error("Client side error [{0}]", (info as UnknownCommandResponseInfo)?.Message);
                throw new Exception("Bad request");
            }
        }
    }
}
