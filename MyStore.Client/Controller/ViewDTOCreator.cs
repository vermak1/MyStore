using MyStore.CommonLib;

namespace MyStore.Client
{
    internal class ViewDTOCreator
    {
        public ViewCarsContainer ConvertCarList(ListCarsResponseInfo info)
        {
            ViewCarsContainer container = new ViewCarsContainer();
            foreach (CarInfo i in info.CarInfos)
            {
                container.CarsInfo.Add(new ViewCarInfo()
                {
                    Brand = i.Brand,
                    Color = i.Color,
                    Country = i.Country,
                    Model = i.Model,
                    Year = i.Year,   
                });
            }
            return container;
        }
    }
}
