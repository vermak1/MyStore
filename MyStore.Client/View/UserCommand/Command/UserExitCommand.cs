using System;

namespace MyStore.Client
{
    internal class UserExitCommand : UserCommand
    {
        public override EUserCommand CommandType => EUserCommand.Exit;
    }
}
