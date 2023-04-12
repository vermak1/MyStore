using MyStore.CommonLib;
using System;
using System.Linq;
using System.Text;

namespace MyStore.Client
{
    internal class ListCarsResponseProcessor
    {
        public static String Convert(ListCarsResponseInfo info)
        {
            var cars = info.CarInfos.OrderByDescending(x => x.Year);

            StringBuilder sb = new StringBuilder();
            sb.Append("List of available cars:");

            int i = 1;
            foreach(var car in cars)
            {
                sb.Append($"\n{i}) Brand: {car.Brand}" +
                          $"\n Model: {car.Model}" +
                          $"\n Year: {car.Year}" +
                          $"\n Country : {car.Country} " +
                          $"\n Color: {car.Color}");
                i++;
            }
            return sb.ToString();
        }
    }
}
