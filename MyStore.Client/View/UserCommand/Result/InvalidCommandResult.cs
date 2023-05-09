using System;

namespace MyStore.Client
{
    internal class InvalidCommandResult : IResult
    {
        public EResultStatus Status => EResultStatus.Warning;

        public string Message => "Command is not valid for state";

        public string Content => String.Empty;
    }
}
