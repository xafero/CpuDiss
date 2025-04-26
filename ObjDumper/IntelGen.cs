using System;
using System.Collections.Generic;
using System.Linq;
using ObjDumper.Core;
using System.Diagnostics;
using System.IO;

namespace ObjDumper
{
    public sealed class IntelGen
    {
        public IntelGen(string outDir)
        {
            OutDir = outDir;
        }

        public string OutDir { get; }

        public IEnumerable<X86> Codes => Enum.GetValues<X86>().Except([X86.None]);
        public IEnumerable<string> CodeNames => Codes.Select(ToName);

        private static void StartI86(byte[] bytes, IDictionary<string, ParsedLine> res,
            string dir, string id)
        {
            if (res.ContainsKey(id)) return;
            var name = Path.Combine(dir, $"i86_{id}.bin");
            File.WriteAllBytes(name, bytes);
            var info = new ProcessStartInfo
            {
                FileName = "objdump",
                ArgumentList = { "-D", "-Mintel,i8086", "-b", "binary", "-m", "i386", "-z", name },
            };
            if (!File.Exists(name))
                return;
            var itm = info.StartAndGet();
            if (itm != null)
                res[id] = itm;
            File.Delete(name);
        }

        public static string ToName(X86 arg)
        {
            var txt = arg.ToString();
            txt = txt.ToLowerInvariant();
            return txt;
        }

        public void Generate()
        {

            
        }
    }
}