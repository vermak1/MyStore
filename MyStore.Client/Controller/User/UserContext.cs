﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class UserContext : IUserStateSwitcher
    {
        public Boolean IsExitRequested { get; private set; }

        private UserStateBase _currentState;

        private readonly List<UserStateBase> _allStates;

        private readonly IController _controller;

        private readonly ILogger _logger;
        public UserContext(IController controller)
        {
            _logger = Configurator.Instance.GetLogger();
            _allStates = new List<UserStateBase>
            {
                new UnauthorizedUser(this),
                new AuthorizedUser(this),
            };
            _currentState = _allStates[0];
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        public void SwitchState<T>() where T : UserStateBase
        {
            _currentState = _allStates.FirstOrDefault(x => x is T);
        }

        public List<String> GetAvailableCommands()
        {
            return _currentState.GetAvailableCommands();
        }

        public async Task<IResult> ProcessCommand(UserCommand command)
        {
            _logger.Info("Command [{0}] received from user", command.CommandType);
            if (command.CommandType == EUserCommand.Exit)
            {
                IsExitRequested = true;
                return ResultFactory.Exit();
            }

            if (command.CommandType == EUserCommand.Unknown)
                return ResultFactory.UnknownCommand();

            if (!_currentState.IsCommandValidForState(command))
                return ResultFactory.InvalidForState();

            IResult result;
            switch (command)
            {
                case UserListAllCarsCommand c:
                    result = await _controller.GetAllCarsCommand(c);
                    break;
                case UserLoginCommand _:
                    result = ResultFactory.Success();
                    break;
                case UserLogoffCommand _:
                    result = ResultFactory.Success();
                    break;
                default:
                    return ResultFactory.InvalidForState();
            }
            _currentState.ChangeStateIfNeeded(command, result);
            return result;
        }

    }
}
