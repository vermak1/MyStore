using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore.Client
{
    internal abstract class UserStateBase
    {
        protected readonly IUserStateSwitcher _stateSwitcher;

        protected readonly ILogger _logger;
        protected abstract EUserCommand[] VALID_COMMANDS { get; }

        public UserStateBase(IUserStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
            _logger = Configurator.Instance.GetLogger();
        }

        public List<String> GetAvailableCommands()
        {
            return VALID_COMMANDS.Select(a => a.ToString()).ToList();
        }

        public Boolean IsCommandValidForState(UserCommand command)
        {
            return VALID_COMMANDS.Where(x => x == command.CommandType).Any();
        }

        public abstract void ChangeStateIfNeeded(UserCommand command, IResult result);
    }
}
