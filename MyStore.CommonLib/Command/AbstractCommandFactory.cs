using System;

namespace MyStore.CommonLib
{
    public abstract class AbstractCommandFactory
    {
        protected readonly ISerializer _serializer;

        public AbstractCommandFactory()
        {
            _serializer = new JsonSerializer();
        }
    }
}
