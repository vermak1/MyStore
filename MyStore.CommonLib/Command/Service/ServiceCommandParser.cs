using System;

namespace MyStore.CommonLib
{
    public class ServiceCommandParser : AbstractCommandParser
    {
        public LibVersionCommand GetLibVersionCommand(String stringObj)
        {
            if (String.IsNullOrEmpty(stringObj))
                throw new ArgumentException(nameof(stringObj));

            return _serializer.DeserializeObject<LibVersionCommand>(stringObj);
        }

    }
}
