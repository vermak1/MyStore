using System;
using System.Collections.Generic;

namespace MyStore.Client
{
    internal static class CommandTypeMapper
    {
        private static readonly Dictionary<String, EUserCommand> dict = new Dictionary<String, EUserCommand>
            {
                { "unknown",     EUserCommand.Unknown      },
                { "exit",        EUserCommand.Exit         },
                { "createuser",  EUserCommand.CreateUser   },
                { "login",       EUserCommand.Login        },
                { "logoff",      EUserCommand.Logoff       },
                { "listallcars", EUserCommand.ListAllCars  },
            };

        public static EUserCommand GetCommandType(String input)
        {
            String lower = input.ToLower();
            if (dict.ContainsKey(lower))
                return dict[lower];

            return EUserCommand.Unknown;
        }
    }
}
