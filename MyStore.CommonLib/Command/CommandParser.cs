using System;

namespace MyStore.CommonLib
{
    public class CommandParser : AbstractCommandParser
    {
        public CommandInfo GetCommandInfo(String command)
        {
            if (String.IsNullOrEmpty(command))
                throw new ArgumentException(nameof(command));

            return _serializer.DeserializeObject<CommandInfo>(command);
        }
    }
}
