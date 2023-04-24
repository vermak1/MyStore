using System;

namespace MyStore.Server
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                MainProcessor main = new MainProcessor();
                main.StartServer();
            }
            catch(Exception ex)
            {
                Log.Exception(ex, "Application is closed with code 1");
                Environment.Exit(1);
            }
        }
    }
}
