using System;

namespace MyStore.CommonLib
{
    internal class UnknownCommandResponse : ResponseInfo
    {
        public UnknownCommandResponse()
        {
            Type = ECommandType.Unknown;
        }

        public String Message { get; set; }
    }
}
