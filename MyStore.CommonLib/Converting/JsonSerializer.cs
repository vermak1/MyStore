using System;
using Newtonsoft.Json;

namespace MyStore.CommonLib
{
    internal class JsonSerializer : ISerializer
    {
        public String SerializeObject(Object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            return JsonConvert.SerializeObject(obj);
        }

        public T DeserializeObject<T>(string json)
        {
            if (String.IsNullOrEmpty(json))
                throw new ArgumentException(nameof(json));

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
