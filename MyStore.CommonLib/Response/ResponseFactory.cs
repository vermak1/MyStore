using System;
using System.Collections.Generic;

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
    }
}
