using System;
using System.Collections.Generic;
using System.Linq;
using ObjDumper.Core;
using System.Diagnostics;
using System.IO;
using D = System.Collections.Generic.IDictionary<string, ObjDumper.ParsedLine>;

namespace ObjDumper
{
    public sealed class HitachiGen
    {
        public HitachiGen(string outDir)
        {
            OutDir = outDir;
        }

        public string OutDir { get; }

        public IEnumerable<Sh3> Codes => Enum.GetValues<Sh3>().Except([Sh3.None]);
        public IEnumerable<string> CodeNames => Codes.Select(ToName);

        private static void StartSh3(byte[] bytes, D res,
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
            var itm = info.StartAndGet();
            if (itm != null)
                res[id] = itm;
            File.Delete(name);
        }

        public static string ToName(Sh3 arg)
        {
            var txt = arg.ToString();
            if (txt.EndsWith('s'))
                txt = txt[..^1] + ".S";
            if (txt.EndsWith("_B"))
                txt = txt[..^2] + ".B";
            if (txt.EndsWith("_W"))
                txt = txt[..^2] + ".W";
            if (txt.EndsWith("_L"))
                txt = txt[..^2] + ".L";
            if (txt.StartsWith("CMP_"))
                txt = txt.Replace('_', '/');
            txt = txt.ToLowerInvariant();
            return txt;
        }

        public D Generate()
        {
            const string name = "sh3.json";

            var res = JsonTool.Load<SortedDictionary<string, ParsedLine>>(OutDir, name);
            Console.WriteLine($"Loading {res.Count} entries from '{name}'!");

            for (var i = 0; i < ushort.MaxValue + 1; i++)
                Generate(i, res);

            Console.WriteLine($"Saving {res.Count} entries for '{name}'!");
            JsonTool.Save(OutDir, name, res);

            return res;
        }

        private void Generate(int id, D res)
        {
            var bytes = BitConverter.GetBytes((ushort)id);
            var nr = Convert.ToHexString(bytes);
            StartSh3(bytes, res, OutDir, nr);
        }
    }
}