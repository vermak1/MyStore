using System;

namespace MyStore.CommonLib
{
    public class ResponseParser : AbstractReponseParser
    {
        public ListCarsResponseInfo GetListCarsResponseInfo(String response)
        {
            if (String.IsNullOrEmpty(response))
                throw new ArgumentException(nameof(response));

            return _serializer.DeserializeObject<ListCarsResponseInfo>(response);
        }

        public ResponseInfo GetResponseInfo(String response)
        {
            if (String.IsNullOrEmpty(response))
                throw new ArgumentException(nameof(response));

            return _serializer.DeserializeObject<ResponseInfo>(response);
        }
    }
}
