using System;
using System.Collections.Generic;
using System.Data;

namespace MyStore.Server
{
    internal class DbDataConverter
    {
        public static List<CarContainer> ConvertToListCarContainer(DataSet data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            List<CarContainer> list = new List<CarContainer>();
            if (data.Tables[0].Rows.Count == 0)
            {
                Log.Error("There is not result parsed from DataSet");
                return list;
            }

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                list.Add(new CarContainer()
                {
                    Id = Guid.Parse(data.Tables[0].Rows[i]["id"].ToString()),
                    Brand = data.Tables[0].Rows[i]["brand"].ToString(),
                    Model = data.Tables[0].Rows[i]["model"].ToString(),
                    Color = data.Tables[0].Rows[i]["color"].ToString(),
                    Country = data.Tables[0].Rows[i]["country"].ToString(),
                    Year = Int32.Parse(data.Tables[0].Rows[i]["year"].ToString())
                });
            }
            return list;
        }
    }
}
