using System;


namespace MyStore.CommonLib
{
    public abstract class AbstractCommandParser
    {
        protected readonly ISerializer _serializer;

        public AbstractCommandParser()
        {
            _serializer = new JsonSerializer();
        }
    }
}
