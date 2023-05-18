using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore.Client
{
    internal static class CommandsDescriptor
    {
        private static readonly Dictionary<EUserCommand, String> dict = new Dictionary<EUserCommand, String>
            {
                { EUserCommand.Exit,        "'Exit' - close application"                                                                                                                            },
                { EUserCommand.CreateUser,  "'CreateUser' - create new user"                                                                                                                        },
                { EUserCommand.Login,       "'Login' - login into the store"                                                                                                                        },
                { EUserCommand.Logoff,      "'Logoff' - logoff from the store"                                                                                                                      },
                { EUserCommand.ListAllCars, "'ListAllCars' - list goods in the store\nEnter name and(or) year after the command in case you need to apply filters\nExample: Listallcars audi 2021"  },
            };

        public static String[] GetDescriptions(EUserCommand[] commands)
        {
            if (commands == null)
                throw new ArgumentNullException(nameof(commands));

            return dict.Where(x => commands.Contains(x.Key))
                .Select(x => x.Value)
                .ToArray();
        }
    }
}
