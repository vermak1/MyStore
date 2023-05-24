using System;

namespace MyStore.Server
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                using (MainProcessor main = new MainProcessor())
                {
                    main.StartServer();
                }
            }
            catch(Exception ex)
            {
                Log.Exception(ex);
                Exit(1);
            }
            Exit(0);
        }
        private static void Exit(int code)
        {
            Log.Info($"Application is closed with code {code}");
            Environment.Exit(code);
        }
    }
}
