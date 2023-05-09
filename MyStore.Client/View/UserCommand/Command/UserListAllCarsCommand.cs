using System;


namespace MyStore.Client
{
    internal class UserListAllCarsCommand : UserCommand
    {
        public override EUserCommand CommandType => EUserCommand.ListAllCars;
    }
}
