using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class Program
    {
        private static readonly ILogger _logger = Configurator.Instance.GetLogger();
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
            catch(Exception ex)
            {
                _logger.Exception(ex, "Application is closed");
                Environment.Exit(1);
            }
            
        }
    }
}
