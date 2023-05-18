using System;

namespace MyStore.CommonLib
{
    public class CommandParser : AbstractCommandParser
    {
        public CommandInfo GetCommandInfo(String command)
        {
            if (String.IsNullOrEmpty(command))
                throw new ArgumentException(nameof(command));

            
            var serialized = _serializer.DeserializeObject<CommandInfo>(command);
            switch(serialized.CommandType) 
            {
                case ECommandType.ListAllCars:
                    return _serializer.DeserializeObject<ListCarCommand>(command);
                case ECommandType.ListAllCarsByName:
                    return _serializer.DeserializeObject<ListCarCommand>(command);
                case ECommandType.ListAllCarsByYear:
                    return _serializer.DeserializeObject<ListCarCommand>(command);
                case ECommandType.ListAllCarsByNameAndYear:
                    return _serializer.DeserializeObject<ListCarCommand>(command);
                default:
                    return _serializer.DeserializeObject<CommandInfo>(command);
            }
        }
    }
}
