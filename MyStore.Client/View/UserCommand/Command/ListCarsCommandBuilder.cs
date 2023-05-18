using System;

namespace MyStore.Client
{
    internal class ListCarsCommandBuilder
    {
        private readonly UserListAllCarsCommand _userCommand;
        private const Int32 YEAR_OF_FIRST_CAR = 1886;
        public ListCarsCommandBuilder()
        {
            _userCommand = new UserListAllCarsCommand();
        }

        public ListCarsCommandBuilder Name(String name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            _userCommand.Model = name;
            return this;
        }

        public ListCarsCommandBuilder Year(Int32 year)
        {
            if (year <= YEAR_OF_FIRST_CAR || year > DateTime.Now.Year)
                throw new ArgumentException(String.Format("Year should be more than {0} and less than {1}", YEAR_OF_FIRST_CAR, DateTime.Now.Year));

            _userCommand.Year = year;
            return this;
        }

        public ListCarsCommandBuilder SubType(EListCarsSubType subType)
        {
            _userCommand.SubType = subType;
            return this;
        }

        public UserListAllCarsCommand Build()
        {
            return _userCommand;
        }
    }
}
