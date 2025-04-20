using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ObjDumper
{
    internal static class JsonTool
    {
        private static readonly JsonSerializerSettings Config = new()
        {
            Converters = { new StringEnumConverter() },
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        public static void Save(IDictionary<string, ParsedLine> dict, string name, string dir)
        {
            var sort = new SortedDictionary<string, ParsedLine>(dict);
            var json = JsonConvert.SerializeObject(sort, Config);
            var path = Path.Combine(dir, name);
            File.WriteAllText(path, json, Encoding.UTF8);
            Console.WriteLine($"{sort.Count} entries written to {name}!");
        }

        public static ConcurrentDictionary<string, ParsedLine> Load(string name, string dir)
        {
            var path = Path.Combine(dir, name);
            var json = File.Exists(path) ? File.ReadAllText(path, Encoding.UTF8) : "{}";
            var sort = JsonConvert.DeserializeObject<SortedDictionary<string, ParsedLine>>(json, Config);
            var dict = new ConcurrentDictionary<string, ParsedLine>(sort);
            Console.WriteLine($"{sort.Count} entries read from {name}!");
            return dict;
        }
    }
}