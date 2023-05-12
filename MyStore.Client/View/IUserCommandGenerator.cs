using System;

namespace MyStore.Client
{
    internal interface IUserCommandGenerator
    {
        UserCommand GetUserCommandFromInput(String input);
    }
}
