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
                IUserInterface ui = new ConsoleUserInterface();
                using (IController controller = new Controller())
                {
                    await ui.Run(controller);
                }
            }
            catch
            {
                Environment.Exit(1);
            }
            
        }
    }
}
