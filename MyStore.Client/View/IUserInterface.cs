using System;
using System.Collections.Generic;

namespace MyStore.Client
{
    internal interface IUserInterface
    {
        void ShowMessage(String message);

        void ShowMessage(String message, params object[] args);

        void ShowMessage(IEnumerable<String> message);

        String GetMessageFromUser();

        void OnCommandHandled(object sender, UserCommandHandledArgs args);

        void OnErrorOccured(object sender, ErrorEventArgs args);

        void OnConnectedToServer(object sender, EventArgs args);
    }
}
