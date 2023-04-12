using System;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class ResponseProcessor
    {
        private readonly ResponseParser _parser;

        public ResponseProcessor()
        {
            _parser = new ResponseParser();
        }

        public String ProcessResponse(String response)
        {
            ResponseInfo responseInfo = _parser.GetResponseInfo(response);

            switch (responseInfo.Type)
            {
                case ECommandType.ListAllCars:
                    ListCarsResponseInfo info = _parser.GetListCarsResponseInfo(response);
                    return ListCarsResponseProcessor.Convert(info);

                default:
                    return "Unknown response received from server";
            }
        }
    }
}
