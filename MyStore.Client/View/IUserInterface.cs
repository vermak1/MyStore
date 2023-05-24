using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal interface IUserInterface
    {
        Task Run();

        void ShowMessage(String message);

        void ShowMessage(String message, params object[] args);

        void ShowMessage(IEnumerable<String> message);

        void ShowAvailableCommands();

        String GetMessageFromUser();
    }
}
