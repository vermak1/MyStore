using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Client
{
    internal class UserContext : IUserStateSwitcher
    {
        private UserStateBase _currentState;

        private readonly List<UserStateBase> _allStates;

        private readonly IController _controller;
        public UserContext(IController controller)
        {
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
