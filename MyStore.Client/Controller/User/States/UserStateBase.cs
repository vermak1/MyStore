using System;
using System.Linq;

namespace MyStore.Client
{
    internal abstract class UserStateBase
    {
        protected readonly IUserStateSwitcher _stateSwitcher;

        protected readonly ILogger _logger;
        public abstract EUserCommand[] VALID_COMMANDS { get; }

        public UserStateBase(IUserStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher ?? throw new ArgumentNullException(nameof(stateSwitcher));
            _logger = Configurator.Instance.GetLogger();
        }

        public Boolean IsCommandValidForState(UserCommand command)
        {
            return VALID_COMMANDS.Where(x => x == command.CommandType).Any();
        }

        public abstract void ChangeStateIfNeeded(UserCommand command, IResult result);
    }
}
