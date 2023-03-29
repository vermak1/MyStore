using System;

namespace MyStore.CommonLib
{
    public class CommandInfo
    {
        public ECommandType CommandType { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
    }
}
