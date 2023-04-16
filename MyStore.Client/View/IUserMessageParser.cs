using System;

namespace MyStore.Client
{
    internal interface IUserMessageParser
    {
        UserCommand GetUserCommandFromInput(String input);
    }
}
