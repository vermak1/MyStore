using System;

namespace MyStore.CommonLib
{
    public class ResponseFactory : AbstractResponseFactory
    {
        public String ResponseListAllCars(ListCarsResponseInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            return _serializer.SerializeObject(info);
        }

        public String UnknownCommand(String message)
        {
            if (message == null) 
                throw new ArgumentNullException();

            var response = new UnknownCommandResponse
            {
                Message = message
            };
            return _serializer.SerializeObject(response);
        }
    }
}
