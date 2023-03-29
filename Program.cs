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
                MainProcessor main = new MainProcessor();
                await main.StartServer();
            }
            catch
            {
                Environment.Exit(1);
            }
        }
    }
}
