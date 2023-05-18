using System;

namespace MyStore.Client
{
    internal class AuthorizedUser : UserStateBase
    {
        public AuthorizedUser(IUserStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
            _validCommands = new EUserCommand[]
            {
                EUserCommand.Exit,
                EUserCommand.Logoff,
                EUserCommand.ListAllCars,
            };
        }

        private readonly EUserCommand[] _validCommands;

        public override EUserCommand[] VALID_COMMANDS => _validCommands;

        public override void ChangeStateIfNeeded(UserCommand command, IResult result)
        {
            if (command.CommandType == EUserCommand.Logoff && result.Status == EResultStatus.Success)
            {
                _stateSwitcher.SwitchState<UnauthorizedUser>();
                _logger.Info("State was changed from '{0}' to '{1}'", nameof(AuthorizedUser), nameof(UnauthorizedUser));
            }
        }
    }
}
