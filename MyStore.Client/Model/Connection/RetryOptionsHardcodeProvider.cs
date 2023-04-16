using System;

namespace MyStore.Client
{
    internal class RetryOptionsHardcodeProvider : IRetryOptionsProvider
    {
        public TimeSpan RetryInterval => TimeSpan.FromSeconds(3);

        public int RetryCount => 3;
    }
}
