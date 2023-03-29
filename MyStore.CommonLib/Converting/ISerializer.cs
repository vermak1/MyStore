using System;

namespace MyStore.CommonLib
{
    public interface ISerializer
    {
        String SerializeObject(Object obj);
        T DeserializeObject<T>(String stringObj);
    }
}
