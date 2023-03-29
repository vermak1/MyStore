using MyStore.CommonLib;
using System;
using System.Threading.Tasks;

namespace MyStore.Server.Commands
{
    internal class CommandExecutor
    {
        public static async Task ExecuteCommandAsync(CommandInfo command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            try
            {
                switch(command.CommandType)
                {
                    case ECommandType.CreateCustomer:
                        Customer c = Customer.CreateForSQL();
                        await c.CreateCustomerAsync(command.CustomerInfo);
                        break;

                    default:
                        throw new NotImplementedException();
                }
                Console.WriteLine("Command [{0}] was executed", command.CommandType);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in command execution\nError: {0}", ex.Message);
            }
        }
    }
}
