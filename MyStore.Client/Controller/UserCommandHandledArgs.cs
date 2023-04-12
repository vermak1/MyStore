using System;

namespace MyStore.Client
{
    internal class UserCommandHandledArgs : EventArgs
    {
        public String Message { get; }

        public EUserCommand EUserCommand { get; }

        public UserCommandHandledArgs(string message, EUserCommand eUserCommand)
        {
            Message = message;
            EUserCommand = eUserCommand;
        }
    }
}
