using System;


namespace MyStore.CommonLib
{
    public class AbstractReponseParser
    {
        protected readonly ISerializer _serializer;

        public AbstractReponseParser()
        {
            _serializer = new JsonSerializer();
        }
    }
}
