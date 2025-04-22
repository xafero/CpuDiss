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

        private static void Generate(string dir, string id, IDictionary<string, ParsedLine> res,
            Action<Assembler, byte> action)
        {
            for (var i = 0; i <= byte.MaxValue; i++)
            {
                var val = i;
                Generate(dir, $"{id}_{val:X2}", res, c => action(c, (byte)val));
            }
        }

        private static void Generate(string dir, string id, IDictionary<string, ParsedLine> res,
            Action<Assembler, AssemblerRegister8, AssemblerRegister8> action)
        {
            Register[] reg8 = [Register.AH, Register.BH, Register.CH, Register.DH];
            foreach (var a in reg8)
            foreach (var b in reg8)
            {
                var val = $"{a}_{b}";
                Generate(dir, $"{id}_{val}", res, c => action(c,
                    new AssemblerRegister8(a), new AssemblerRegister8(b)));
            }
        }

        private static void Generate(string dir, string id, IDictionary<string, ParsedLine> res,
            Action<Assembler, AssemblerRegister16> action)
        {
            Register[] reg16 = [Register.AX, Register.BX, Register.CX, Register.DX];
            foreach (var a in reg16)
            {
                var val = $"{a}";
                Generate(dir, $"{id}_{val}", res, c => action(c,
                    new AssemblerRegister16(a)));
            }
        }

        public static void Generate(string dir)
        {
            var res = new SortedDictionary<string, ParsedLine>();
            Generate(dir, "aaa", res, c => c.aaa());
            Generate(dir, "aad", res, (c, x) => c.aad(x));
            Generate(dir, "aam", res, (c, x) => c.aam(x));
            Generate(dir, "aas", res, c => c.aas());
            Generate(dir, "adc", res, (c, x, y) => c.adc(x, y));
            Generate(dir, "add", res, (c, x, y) => c.add(x, y));
            Generate(dir, "and", res, (c, x, y) => c.and(x, y));
            Generate(dir, "call", res, (Assembler c, AssemblerRegister16 x) => c.call(x));
            Generate(dir, "cbw", res, c => c.cbw());
            Generate(dir, "clc", res, c => c.clc());
            Generate(dir, "cld", res, c => c.cld());
            Generate(dir, "cli", res, c => c.cli());
            Generate(dir, "cmc", res, c => c.cmc());

            JsonTool.Save(dir, "i86.json", res);
        }
    }
}