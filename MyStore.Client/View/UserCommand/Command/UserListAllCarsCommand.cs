using System;

namespace MyStore.Client
{
    internal class UserListAllCarsCommand : UserCommand
    {
        public override EUserCommand CommandType => EUserCommand.ListAllCars;
        public EListCarsFilter Filter { get; set; }
        public String Model { get; set; }
        public Int32 Year { get; set; }
    }
}
