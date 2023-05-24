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

        public ListCarsCommandBuilder Model(String model)
        {
            if (String.IsNullOrEmpty(model))
                throw new ArgumentException();

            _userCommand.Model = model;
            return this;
        }

        public ListCarsCommandBuilder Year(Int32 year)
        {
            if (year <= YEAR_OF_FIRST_CAR || year > DateTime.Now.Year)
                throw new ArgumentException(String.Format("Year should be more than {0} and less than {1}", YEAR_OF_FIRST_CAR, DateTime.Now.Year));

            _userCommand.Year = year;
            return this;
        }

        public ListCarsCommandBuilder SubType(EListCarsFilter filter)
        {
            _userCommand.Filter = filter;
            return this;
        }

        public UserListAllCarsCommand Build()
        {
            return _userCommand;
        }
    }
}
