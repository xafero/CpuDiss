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

        public static void Save(string dir, string name, object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Config);
            var path = Path.Combine(dir, name);
            File.WriteAllText(path, json, Encoding.UTF8);
        }
    }
}