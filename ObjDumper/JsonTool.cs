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

        public static T Load<T>(string dir, string name)
        {
            var path = Path.Combine(dir, name);
            var json = File.Exists(path) ? File.ReadAllText(path, Encoding.UTF8) : "{}";
            return JsonConvert.DeserializeObject<T>(json, Config);
        }
    }
}