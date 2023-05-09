using System;

namespace MyStore.Client
{
    internal class UserUnknownCommand : UserCommand
    {
        public override EUserCommand CommandType => EUserCommand.Unknown;
    }
}
