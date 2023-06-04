using System;

namespace MyStore.CommonLib
{
    public class CommandFactory : AbstractCommandFactory
    {
        public String ListAllCarsCommand()
        {
            ListCarCommand command = new ListCarCommand()
            {
                CommandType = ECommandType.ListAllCars
            };

            return _serializer.SerializeObject(command);
        }

        public String ListCarsByName(String name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            ListCarCommand command = new ListCarCommand()
            {
                CommandType = ECommandType.ListAllCarsByName,
                Model = name,
            };
            return _serializer.SerializeObject(command);
        }

        public String ListCarsByNameAndYear(String name, Int32 year)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            ListCarCommand command = new ListCarCommand()
            {
                CommandType = ECommandType.ListAllCarsByNameAndYear,
                Model = name,
                Year = year
            };

            return _serializer.SerializeObject(command);
        }

        public String ListCarsByYear(Int32 year)
        {
            ListCarCommand command = new ListCarCommand()
            {
                CommandType = ECommandType.ListAllCarsByYear,
                Year = year
            };

            return _serializer.SerializeObject(command);
        }

        public CommandInfo GetCommandType(String command)
        {
            if (String.IsNullOrEmpty(command))
                throw new ArgumentException(nameof(command));

            return _serializer.DeserializeObject<CommandInfo>(command);
        }

    }
}
