using System;
using System.Collections.Generic;

namespace MyStore.CommonLib
{
    public class ListCarsResponseInfo : ResponseInfo
    {
        public List<CarInfo> CarInfos { get; set; }

        public ListCarsResponseInfo()
        {
            CarInfos = new List<CarInfo>();
        }
    }
}
