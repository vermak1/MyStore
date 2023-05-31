using System;

namespace MyStore.Server
{
    internal class Result : IResult
    {
        public string Message { get; set ; }
        public EResultStatus Status { get; set; }
    }
}
