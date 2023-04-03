using System;


namespace MyStore.CommonLib
{
    public class AbstractResponseParser
    {
        protected readonly ISerializer _serializer;

        public AbstractResponseParser()
        {
            _serializer = new JsonSerializer();
        }
    }
}
