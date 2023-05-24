using MyStore.CommonLib;
using System.Collections.Generic;

namespace MyStore.Server
{
    internal class ContainerConverter
    {
        public static ListCarsResponseInfo ConvertFromServerListToCommonLib(List<CarContainer> container)
        {
            ListCarsResponseInfo info = new ListCarsResponseInfo();

            foreach (CarContainer car in container)
            {
                info.CarInfos.Add(new CarInfo()
                {
                    Id= car.Id,
                    Brand= car.Brand,
                    Model= car.Model,
                    Color= car.Color,
                    Country= car.Country,
                    Year= car.Year,
                });
            }
            return info;
        }
    }
}
