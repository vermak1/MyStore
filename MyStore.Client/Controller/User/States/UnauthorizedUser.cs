using System;

namespace MyStore.Client
{
    internal class UnauthorizedUser : UserStateBase
    {
        public UnauthorizedUser(IUserStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
        }

        protected override EUserCommand[] VALID_COMMANDS => new EUserCommand[] 
        {
            EUserCommand.Login,
            EUserCommand.CreateUser
        };

        public override void ChangeStateIfNeeded(UserCommand command, IResult result)
        {
            if (command.CommandType == EUserCommand.Login && result.Status == EResultStatus.Success)
                _stateSwitcher.SwitchState<AuthorizedUser>();
        }
    }
}
