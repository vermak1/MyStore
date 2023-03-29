using System;
using System.Collections.Generic;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class CommandsProvider
    {
        public static List<String> GetAvailableCommands()
        {
            List<String> commands = new List<String>();
            foreach(string name in Enum.GetNames(typeof(ECommandType)))
                commands.Add(name);
            
            return commands;
        }
    }
}
