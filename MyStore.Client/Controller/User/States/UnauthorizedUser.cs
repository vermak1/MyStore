using System;

namespace MyStore.Client
{
    internal class UnauthorizedUser : UserStateBase
    {
        public UnauthorizedUser(IUserStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
            _validCommands = new EUserCommand[]
            {
                EUserCommand.Exit,
                EUserCommand.Login,
                EUserCommand.CreateUser,
            };
        }

        private readonly EUserCommand[] _validCommands;
        public override EUserCommand[] VALID_COMMANDS => _validCommands;

        public override void ChangeStateIfNeeded(UserCommand command, IResult result)
        {
            if (command.CommandType == EUserCommand.Login && result.Status == EResultStatus.Success)
            {
                _stateSwitcher.SwitchState<AuthorizedUser>();
                _logger.Info("State was changed from '{0}' to '{1}'", nameof(UnauthorizedUser), nameof(AuthorizedUser));
            }
        }
    }
}
