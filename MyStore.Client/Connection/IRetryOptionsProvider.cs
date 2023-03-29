using System;

namespace MyStore.Client
{
    internal interface IRetryOptionsProvider
    {
        TimeSpan RetryInterval { get; }

        Int32 RetryCount { get; }
    }
}
