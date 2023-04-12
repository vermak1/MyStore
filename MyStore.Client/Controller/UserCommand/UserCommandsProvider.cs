using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore.Client
{
    internal class UserCommandsProvider
    {
        public static List<String> GetAvailableCommands()
        {
            return Enum.GetNames(typeof(EUserCommand)).Where(x => x != "Unknown").ToList();
        }
    }
}
