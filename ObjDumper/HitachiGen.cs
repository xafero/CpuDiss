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
            var bytes = BitConverter.GetBytes(id);
            StartSh3(bytes, res, dir, $"{id:X4}");
        }

        public static void Generate(string dir)
        {
            var res = new SortedDictionary<string, ParsedLine>();
            Generate(dir, 90, res);
            JsonTool.Save(dir, "sh3.json", res);
        }
    }
}