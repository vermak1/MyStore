using System;

namespace MyStore.CommonLib
{
    public abstract class AbstractResponseFactory
    {
        protected readonly ISerializer _serializer;

        public AbstractResponseFactory()
        {
            _serializer = new JsonSerializer();
        }
    }
}
