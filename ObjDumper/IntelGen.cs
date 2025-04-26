using System;
using System.Collections.Generic;
using System.Linq;
using ObjDumper.Core;
using System.Diagnostics;
using System.IO;
using Iced.Intel;
using D = System.Collections.Generic.IDictionary<string, ObjDumper.ParsedLine>;

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

        private static void StartI86(byte[] bytes, D res,
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
            const string name = "i86.json";

            var res = JsonTool.Load<SortedDictionary<string, ParsedLine>>(OutDir, name);
            Console.WriteLine($"Loading {res.Count} entries from '{name}'!");

            for (var i = 0; i < ushort.MaxValue + 1; i++)
                Generate(i, res);

            GenerateExtra(res);

            Console.WriteLine($"Saving {res.Count} entries for '{name}'!");
            JsonTool.Save(OutDir, name, res);
        }

        private void Generate(int id, D res)
        {
            var bytes = BitConverter.GetBytes((ushort)id);
            Generate(bytes, res);
        }

        private void Generate(byte[] bytes, D res)
        {
            var nr = Convert.ToHexString(bytes);
            StartI86(bytes, res, OutDir, nr);
        }

        private void Do(D res, Action<Assembler> action)
        {
            var asm = IntelTool.CreateAss();
            action(asm);
            Generate(asm.GetBytes(), res);
        }

        private void GenerateExtra(D r)
        {
            Do(r, a => a.daa());
        }
    }
}