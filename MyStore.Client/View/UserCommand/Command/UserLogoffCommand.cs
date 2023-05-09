using System;

namespace MyStore.Client
{
    internal class UserLogoffCommand : UserCommand
    {
        public override EUserCommand CommandType => EUserCommand.Logoff;
    }
}
