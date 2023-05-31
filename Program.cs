using System;
using System.Threading.Tasks;

namespace MyStore.Server
{
    internal class Program
    {
        static async Task Main()
        {
            try
            {
                using (MainProcessor main = new MainProcessor())
                {
                    await main.StartServer();
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
