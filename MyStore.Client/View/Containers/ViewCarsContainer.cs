using System;
using System.Collections.Generic;
using System.Text;

namespace MyStore.Client
{
    internal class ViewCarsContainer
    {
        public List<ViewCarInfo> CarsInfo { get; set; }

        public ViewCarsContainer()
        {
            CarsInfo = new List<ViewCarInfo>();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("List of available cars:");

            int i = 1;
            foreach (var car in CarsInfo)
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