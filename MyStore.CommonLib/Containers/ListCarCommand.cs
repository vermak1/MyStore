using System;

namespace MyStore.CommonLib
{
    public class ListCarCommand : CommandInfo
    {
        public String Model { get; set; }

        public Int32 Year {get; set; }
    }
}
