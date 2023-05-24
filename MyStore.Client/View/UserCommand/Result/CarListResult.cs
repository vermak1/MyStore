using System;

namespace MyStore.Client
{
    internal class CarListResult : IResult
    {
        public EResultStatus Status { get; set;}

        public string Message { get; set; }

        public string Content { get; set;}
    }
}
