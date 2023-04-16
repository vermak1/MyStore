using System;

namespace MyStore.Client
{
    internal abstract class UserCommand
    {
        public EUserCommand CommandType { get; set; }
    }
}
