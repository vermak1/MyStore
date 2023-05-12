using System;

namespace MyStore.Client
{
    internal class AuthorizedUser : UserStateBase
    {
        public AuthorizedUser(IUserStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
        }

        protected override EUserCommand[] VALID_COMMANDS => new EUserCommand[]
        {
            EUserCommand.Logoff,
            EUserCommand.ListAllCars
        };

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
