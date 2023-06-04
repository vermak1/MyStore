using System;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class Program
    {
        private static readonly ILogger _logger = Configurator.Instance.GetLogger();
        static async Task Main()
        {
            _logger.Info("MyStore Client is started");
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
                _logger.Exception(ex);
                Exit(1);
            }
            Exit(0);
        }
        
        private static void Exit(int exitCode)
        {
            _logger.Info("Application is closed with code {0}", exitCode);
            Environment.Exit(exitCode);
        }
    }
}
