using MyStore.CommonLib;
using System.Collections.Generic;

namespace MyStore.Server
{
    internal class ContainerConverter
    {
        public static ListCarsResponseInfo ConvertFromServerListToCommonLib(List<CarContainer> container, ECommandType type)
        {
            ListCarsResponseInfo info = new ListCarsResponseInfo()
            {
                Code = EResponseCode.Success,
                Type = type,
            };

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
