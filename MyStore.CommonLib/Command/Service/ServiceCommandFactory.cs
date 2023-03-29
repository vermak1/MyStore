using System;

namespace MyStore.CommonLib
{
    public class ServiceCommandFactory : AbstractCommandFactory
    {
        public String GetLibraryVersionCommand()
        {
            LibVersionCommand command = new LibVersionCommand()
            {
                ServiceCommand = EServiceCommand.GetLibraryVersion
            };

            return _serializer.SerializeObject(command);
        }

        public String ReturnLibraryVersionCommand()
        {
            LibVersionCommand command = new LibVersionCommand()
            {
                ServiceCommand = EServiceCommand.ReturnLibraryVersion,
            };

            return _serializer.SerializeObject(command);
        }
    }
}
