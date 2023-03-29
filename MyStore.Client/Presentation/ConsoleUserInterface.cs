using System;
using System.Collections.Generic;

namespace MyStore.Client
{
    internal class ConsoleUserInterface : IUserInterface
    {
        public String GetMessageFromUser()
        {
            return Console.ReadLine();
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine("[{0}]\t{1}", DateTime.Now, message);
        }

        public void ShowMessage(IEnumerable<string> message)
        {
            foreach (var line in message)
                ShowMessage(line);
        }
    }
}
