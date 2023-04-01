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
            if(String.IsNullOrEmpty(message))
                throw new ArgumentException(nameof(message));

            Console.WriteLine("[{0}]\t{1}", DateTime.Now, message);
        }

        public void ShowMessage(IEnumerable<string> message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            foreach (var line in message)
                ShowMessage(line);
        }

        public void ShowMessage(string message, params object[] args)
        {
            if (args == null)
            {
                ShowMessage(message);
                return;
            }

            String formatted = String.Format(message, args);
            ShowMessage(formatted);
        }
    }
}
