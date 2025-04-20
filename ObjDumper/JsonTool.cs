using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ObjDumper
{
    internal static class JsonTool
    {
        public static void Save(IDictionary<long, ParsedLine> dict, string name, string dir)
        {
            var sort = new SortedDictionary<long, ParsedLine>(dict);
            var json = JsonConvert.SerializeObject(sort, new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            });
            var path = Path.Combine(dir, name);
            File.WriteAllText(path, json, Encoding.UTF8);
            Console.WriteLine($"{sort.Count} entries written to {name}!");
        }
    }
}