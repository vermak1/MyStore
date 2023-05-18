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
    }
}
