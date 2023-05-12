using System;

namespace MyStore.Client
{
    internal class ExitResult : IResult
    {
        public EResultStatus Status => EResultStatus.Success;

        public string Message => "Ciao";

        public string Content => String.Empty;
    }
}
