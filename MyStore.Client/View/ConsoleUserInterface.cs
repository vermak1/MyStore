using System;
using System.Collections.Generic;

namespace MyStore.Client
{
    internal class ConsoleUserInterface : IUserInterface
    {
        public String GetMessageFromUser()
        {
            Console.WriteLine(">");
            return Console.ReadLine();
        }

        public void ShowMessage(string message)
        {
            if(message == null)
                throw new ArgumentNullException(nameof(message));

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

        public void OnCommandHandled(object sender, UserCommandHandledArgs args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            ShowMessage(args.Message);
        }

        public void OnErrorOccured(object sender, ErrorEventArgs args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            ShowMessage(args.Message);
            if (args.Exception == null)
                return;
            ShowMessage(args.Exception.Message);
        }

        public void OnConnectedToServer(object sender, EventArgs args)
        {
            ShowMessage("Successfully connected to server");
        }
    }
}
