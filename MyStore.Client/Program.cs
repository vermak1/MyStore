using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class Program
    {
        static async Task Main()
        {
            try
            {
                MainProcessor main = new MainProcessor();
                await main.Start();
            }
            catch
            {
                Environment.Exit(1);
            }
            
        }
    }
}
