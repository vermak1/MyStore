using System;
using System.Text;
using MyStore.CommonLib;

namespace MyStore.Client
{
    internal static class ResultFactory
    {
        public static IResult InvalidForState()
        {
            return new InvalidCommandResult();
        }

        public static IResult CarListResult(EResultStatus status, ListCarsResponseInfo info)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            StringBuilder sb = new StringBuilder();
            int j = 1;
            foreach (CarInfo i in info.CarInfos)
            {
                sb.Append($"{j}) {i.Brand}\n")
                    .Append($"Color: {i.Color}\n")
                    .Append($"Country: {i.Country}\n")
                    .Append($"Model: {i.Model}\n")
                    .Append($"Year: {i.Year}\n");
                j++;
            }
            IResult result = new CarListResult
            {
                Status = status,
                Message = "List of available cars",
                Content = sb.ToString()
            };

            return result;
        }

        public static IResult Success()
        {
            return new SuccessResult();
        }

        public static IResult Error(String message) 
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return new ErrorResult()
            {
                Message = message
            };
        }
    }
}
