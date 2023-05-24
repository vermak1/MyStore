using System;
using System.Collections.Generic;

namespace MyStore.Client
{
    internal interface IResult
    {
        EResultStatus Status { get; }
        String Message { get; }
        String Content { get; }
    }
}
