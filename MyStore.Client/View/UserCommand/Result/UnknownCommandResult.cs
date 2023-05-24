using System;

namespace MyStore.Client
{
    internal class UnknownCommandResult : IResult
    {
        public EResultStatus Status => EResultStatus.Warning;

        public string Message => "Unknown command received";

        public string Content => String.Empty;
    }
}
