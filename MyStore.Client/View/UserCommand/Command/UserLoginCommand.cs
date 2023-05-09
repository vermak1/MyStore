using System;

namespace MyStore.Client
{
    internal class UserLoginCommand : UserCommand
    {
        public override EUserCommand CommandType => EUserCommand.Login;
        public String UserName { get; set; }
    }
}
