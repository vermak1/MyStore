using MyStore.Client;

namespace MyStore.Client
{
    internal class UserInterfaceConfiguration
    {
        public IUserInterface GetDefaultUserInterface()
        {
            return new ConsoleUserInterface();
        }
    }
}
