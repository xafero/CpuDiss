using System;
using System.Collections.Generic;
using System.Linq;
using ObjDumper.Core;
using System.Diagnostics;
using System.IO;

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
            var itm = ObjDumper.ProcTool.StartAndGet(info);
            if (itm != null)
                res[id] = itm;
            File.Delete(name);
        }
    }
}