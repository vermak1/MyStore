using System;

namespace MyStore.CommonLib
{
    public class ResponseParser : AbstractResponseParser
    {
        public ListCarsResponseInfo GetListCarsResponseInfo(String response)
        {
            if (String.IsNullOrEmpty(response))
                throw new ArgumentException(nameof(response));

            return _serializer.DeserializeObject<ListCarsResponseInfo>(response);
        }

        public ResponseInfo GetResponseInfoFromString(String response)
        {
            if (String.IsNullOrEmpty(response))
                throw new ArgumentException(nameof(response));
            
            var info = _serializer.DeserializeObject<ResponseInfo>(response);
            switch (info.Code)
            {
                case EResponseCode.Success:
                    return _serializer.DeserializeObject<ResponseInfo>(response);
                case EResponseCode.ClientError:
                    return _serializer.DeserializeObject<UnknownCommandResponseInfo>(response);
                case EResponseCode.ServerError:
                    return _serializer.DeserializeObject<ErrorResponseInfo>(response);
                default:
                    throw new Exception("Unknown type of code");
            }
        }
    }
}
