using System;
using Iced.Intel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static ObjDumper.ProcTool;

namespace ObjDumper
{
    public static class IntelGen
    {
        private static Assembler CreateAss()
        {
            return new Assembler(16);
        }

        private static byte[] GetBytes(this Assembler c)
        {
            const ulong ip = 0x0;
            using var stream = new MemoryStream();
            c.Assemble(new StreamCodeWriter(stream), ip);
            return stream.ToArray();
        }

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
            var itm = StartAndGet(info);
            if (itm != null)
                res[id] = itm;
            File.Delete(name);
        }

        private static void Generate(string dir, string id,
            IDictionary<string, ParsedLine> res, Action<Assembler> action)
        {
            var c = CreateAss();
            action(c);
            c.ret();
            var bytes = c.GetBytes();
            StartI86(bytes, res, dir, id);
        }

        private static void Generate(string dir, string id,
            IDictionary<string, ParsedLine> res, Action<Assembler, byte> action)
        {
            for (var i = 0; i <= byte.MaxValue; i++)
            {
                var val = i;
                Generate(dir, $"{id}_{i:X2}", res, c => action(c, (byte)val));
            }
        }

        public static void Generate(string dir)
        {
            var res = new SortedDictionary<string, ParsedLine>();
            Generate(dir, "aaa", res, c => c.aaa());
            Generate(dir, "aad", res, (c, x) => c.aad(x));
            Generate(dir, "aam", res, (c, x) => c.aam(x));
            Generate(dir, "aas", res, c => c.aas());
            JsonTool.Save(dir, "i86.json", res);
        }
    }
}