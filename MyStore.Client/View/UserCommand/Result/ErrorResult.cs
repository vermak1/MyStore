using System;

namespace MyStore.Client
{
    internal class ErrorResult : IResult
    {
        public EResultStatus Status => EResultStatus.Failed;

        public string Message { get; set; }

        public string Content { get; set; }
    }
}
