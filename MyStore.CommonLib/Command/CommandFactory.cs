using System;

namespace MyStore.CommonLib
{
    public class CommandFactory : AbstractCommandFactory
    {
        public String CreateLoginCommand(String mail)
        {
            if (String.IsNullOrEmpty(mail))
                throw new ArgumentException(nameof(mail));

            CommandInfo command = new CommandInfo()
            {
                CommandType = ECommandType.Login,
                CustomerInfo = new CustomerInfo()
                {
                    Mail = mail
                }
            };
            return _serializer.SerializeObject(command);
        }

        public String CreateCreateCustomerCommand(CustomerInfo customerInfo)
        {
            if (customerInfo == null)
                throw new ArgumentException(nameof(customerInfo));

            CommandInfo command = new CommandInfo()
            {
                CommandType = ECommandType.CreateCustomer,
                CustomerInfo = customerInfo
            };
            return _serializer.SerializeObject(command);
        }

        public String ListAllCarsCommand()
        {
            CommandInfo command = new CommandInfo()
            {
                CommandType = ECommandType.ListAllCars
            };

            return _serializer.SerializeObject(command);
        }
    }
}
