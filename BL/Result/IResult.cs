using System;

namespace MyStore.Server
{
    internal interface IResult
    {
        string Message { get; set; }
        EResultStatus Status { get; set; }
    }
}
