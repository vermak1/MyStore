using System;

namespace MyStore.Client
{
    internal class UserErrorCommand : UserCommand
    {
        public String Error { get; set; }
    }
}
