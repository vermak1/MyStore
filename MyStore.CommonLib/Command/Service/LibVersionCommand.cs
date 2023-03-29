using System;

namespace MyStore.CommonLib
{
    public class LibVersionCommand
    {
        public EServiceCommand ServiceCommand { get; set; }

        public Int32 Version => LibraryInfo.Version;

    }
}