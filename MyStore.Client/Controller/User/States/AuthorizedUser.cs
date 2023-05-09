﻿using System;

namespace MyStore.Client
{
    internal class AuthorizedUser : UserStateBase
    {
        public AuthorizedUser(IUserStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
            VALID_COMMANDS = new EUserCommand[]
            {
                EUserCommand.ListAllCars,
                EUserCommand.Logoff,
            };
        }

        public override void ChangeStateIfNeeded(UserCommand command, IResult result)
        {
            if (command.CommandType == EUserCommand.Logoff && result.Status == EResultStatus.Success)
                _stateSwitcher.SwitchState<UnauthorizedUser>();
        }
    }
}