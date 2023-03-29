using System;

namespace MyStore.CommonLib
{
    public class CarInfo
    {
        public Guid Id { get; set; }

        public String Brand { get; set; }

        public String Model { get; set; }

        public String Color { get; set; }

        public String Country { get; set; }

        public Int32 Year { get; set; }
    }
}
