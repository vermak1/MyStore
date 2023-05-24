using System;

namespace MyStore.Client
{
    internal class SuccessResult : IResult
    {
        public EResultStatus Status => EResultStatus.Success;

        public string Message => "Success";

        public string Content => String.Empty;
    }
}
