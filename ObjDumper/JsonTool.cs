using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ObjDumper
{
    internal static class JsonTool
    {
        public static void Save(IEnumerable<ParsedLine> list, string name, string dir)
        {
            var sort = list.OrderBy(l => l.H.Length)
                .ThenBy(l => l.H).Distinct().ToArray();
            var json = JsonConvert.SerializeObject(sort, new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            });
            var path = Path.Combine(dir, name);
            File.WriteAllText(path, json, Encoding.UTF8);
            Console.WriteLine($"{sort.Length} entries written to {name}!");
        }
    }
}