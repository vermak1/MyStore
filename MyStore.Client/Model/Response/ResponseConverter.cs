using System;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class ResponseConverter
    {
        private readonly ResponseParser _parser;

        public ResponseConverter()
        {
            _parser = new ResponseParser();
        }

        public ListCarsResponseInfo ConvertStringToListCars(String response)
        {
            return _parser.GetListCarsResponseInfo(response);
        }
    }
}
