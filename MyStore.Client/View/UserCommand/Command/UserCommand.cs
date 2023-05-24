using System;

namespace MyStore.Client
{
    internal abstract class UserCommand
    {
        public virtual EUserCommand CommandType { get; set; }
    }
}
