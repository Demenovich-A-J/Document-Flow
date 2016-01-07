using System;
using System.Collections.Generic;

namespace BL.DinamicJsonHelper
{
    public class DynamicJsonConverter
    {
        public static List<Tuple<string, string>> Convert(object[] jsonArray)
        {
            var list = new List<Tuple<string, string>>();

            foreach (Dictionary<string, object> item in jsonArray)
            {
                list.Add(new Tuple<string, string>(item["name"].ToString(), item["value"].ToString()));
            }
            return list;
        }
    }
}