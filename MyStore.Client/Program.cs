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
                using (IController controller = new Controller())
                {
                    UserContext context = new UserContext(controller);
                    IUserInterface ui = new ConsoleUserInterface(context);
                    await ui.Run();
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
