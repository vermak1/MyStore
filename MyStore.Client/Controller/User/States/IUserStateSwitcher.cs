using System;

namespace MyStore.Client
{
    internal interface IUserStateSwitcher
    {
        void SwitchState<T>() where T : UserStateBase;
    }
}
