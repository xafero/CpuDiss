using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static ObjDumper.ProcTool;

namespace ObjDumper
{
    public static class HitachiGen
    {
        private static void StartSh3(byte[] bytes, IDictionary<string, ParsedLine> res,
            string dir, string id)
        {
            if (res.ContainsKey(id)) return;
            var name = Path.Combine(dir, $"sh3_{id}.bin");
            File.WriteAllBytes(name, bytes);
            var info = new ProcessStartInfo
            {
                FileName = "sh-elf-objdump",
                ArgumentList = { "-D", "-b", "binary", "-m", "sh3", "-z", name }
            };
            if (!File.Exists(name))
                return;
            var itm = StartAndGet(info);
            if (itm != null)
                res[id] = itm;
            File.Delete(name);
        }

        private static void Generate(string dir, int id,
            IDictionary<string, ParsedLine> res)
        {
            var bytes = BitConverter.GetBytes((ushort)id);
            var nr = Convert.ToHexString(bytes);
            StartSh3(bytes, res, dir, nr);
        }

        public static IDictionary<string, ParsedLine> Generate(string dir)
        {
            const string name = "sh3.json";
            var res = JsonTool.Load<SortedDictionary<string, ParsedLine>>(dir, name);
            Console.WriteLine($"Loading {res.Count} entries from '{name}'!");
            for (var i = 0; i < ushort.MaxValue + 1; i++)
                Generate(dir, i, res);
            Console.WriteLine($"Saving {res.Count} entries for '{name}'!");
            JsonTool.Save(dir, name, res);
            
            return res;
        }
    }
}