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

        private static void GenerateB(string dir, string id, IDictionary<string, ParsedLine> res,
            Action<Assembler, byte> action)
        {
            for (var i = 0; i <= byte.MaxValue; i++)
            {
                var val = i;
                Generate(dir, $"{id}_{val:X2}", res, c => action(c, (byte)val));
            }
        }

        private static void Generate8(string dir, string id, IDictionary<string, ParsedLine> res,
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

        private static void Generate16(string dir, string id, IDictionary<string, ParsedLine> res,
            Action<Assembler, AssemblerRegister16, AssemblerRegister16> action)
        {
            Register[] reg16 = [Register.AX, Register.BX, Register.CX, Register.DX];
            foreach (var a in reg16)
            foreach (var b in reg16)
            {
                var val = $"{a}_{b}";
                Generate(dir, $"{id}_{val}", res, c => action(c,
                    new AssemblerRegister16(a), new AssemblerRegister16(b)));
            }
        }

        private static void Generate16(string dir, string id, IDictionary<string, ParsedLine> res,
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
            GenerateB(dir, "aad", res, (c, x) => c.aad(x));
            GenerateB(dir, "aam", res, (c, x) => c.aam(x));
            Generate(dir, "aas", res, c => c.aas());
            Generate8(dir, "adc", res, (c, x, y) => c.adc(x, y));
            Generate8(dir, "add", res, (c, x, y) => c.add(x, y));
            Generate8(dir, "and", res, (c, x, y) => c.and(x, y));
            Generate16(dir, "call", res, (c, x) => c.call(x));
            Generate(dir, "cbw", res, c => c.cbw());
            Generate(dir, "clc", res, c => c.clc());
            Generate(dir, "cld", res, c => c.cld());
            Generate(dir, "cli", res, c => c.cli());
            Generate(dir, "cmc", res, c => c.cmc());
            Generate8(dir, "cmp", res, (c, x, y) => c.cmp(x, y));
            Generate(dir, "cmps", res, (c) => c.cmpsb());
            Generate(dir, "cwd", res, c => c.cwd());
            Generate(dir, "daa", res, c => c.daa());
            Generate(dir, "das", res, c => c.das());
            Generate16(dir, "dec", res, (c, x) => c.dec(x));
            Generate16(dir, "div", res, (c, x) => c.div(x));
            Generate(dir, "hlt", res, c => c.hlt());
            Generate16(dir, "idiv", res, (c, x) => c.idiv(x));
            Generate16(dir, "imul", res, (c, x) => c.imul(x));
            Generate(dir, "in", res, (c) => c.@in(new AssemblerRegister16(Register.AX), 12));
            Generate16(dir, "inc", res, (c, x) => c.inc(x));
            GenerateB(dir, "int", res, (c, x) => c.@int(x));
            Generate(dir, "into", res, c => c.into());
            Generate(dir, "iret", res, c => c.iret());
            GenerateB(dir, "ja", res, (c, x) => c.ja(x));
            GenerateB(dir, "jnbe", res, (c, x) => c.jnbe(x));

            
            

            
            /*
            Generate(dir, "JAE/JNB short-label
            Generate(dir, "JB/JNAE short-label
            Generate(dir, "JBE/JNA short-label
            Generate(dir, "JC short-label
            Generate(dir, "JCXZ short-label
            Generate(dir, "JE/JZ short-label
            Generate(dir, "JG/JNLE short-label
            Generate(dir, "JGE/JNL short-label
            Generate(dir, "JLE/JNG short-label
            Generate(dir, "JLlJNGE short-label
            Generate(dir, "JMP target
            Generate(dir, "JNC short-label
            Generate(dir, "JNE/JNZ short-label
            Generate(dir, "JNO short-label
            Generate(dir, "JNP/JPO short-label
            Generate(dir, "JNS short-label
            Generate(dir, "JO short-label
            Generate(dir, "JP/JPE short-label
            Generate(dir, "JS short-label
            Generate(dir, "LAHF
            Generate(dir, "LDS dest,source
            Generate(dir, "LEA dest, source
            Generate(dir, "LES dest, source
            Generate(dir, "LOCK
            Generate(dir, "LODSB/W
            Generate(dir, "LOOP short-label
            Generate(dir, "LOOPNZ short-label (or LOOPNE)
            Generate(dir, "LOOPZ short-label (or LOOPE)
            Generate(dir, "MOV dest,source
            Generate(dir, "MOVSB/W (like LDI)
            Generate(dir, "MUL source
            Generate(dir, "NMI*
            Generate(dir, "NEG dest
            Generate(dir, "NOP
            Generate(dir, "NOT dest
            Generate(dir, "OR dest,src
            Generate(dir, "OUT port,accumulator
            Generate(dir, "POP dest
            Generate(dir, "POPF
            Generate(dir, "PUSH source
            Generate(dir, "PUSHF
            Generate(dir, "RCL dest, count
            Generate(dir, "RCR dest, count
            Generate(dir, "REP cmd
            Generate(dir, "REPE / REPZ cmd
            Generate(dir, "REPNE / REPNZ cmd
            Generate(dir, "RET optional-pop-value
            Generate(dir, "ROL dest, count
            Generate(dir, "ROR dest, count
            Generate(dir, "SAHF
            Generate(dir, "SAL dest, count
            Generate(dir, "SAR dest, count
            Generate(dir, "SBB dest, src
            Generate(dir, "SCASB/W
            Generate(dir, "Segment*
            Generate(dir, "SHL dest, count
            Generate(dir, "SHR dest, src
            Generate(dir, "SINGLE STEP
            Generate(dir, "STC
            Generate(dir, "STD
            Generate(dir, "STI
            Generate(dir, "STOSB/W dest-string
            Generate(dir, "SUB dest,src
            Generate(dir, "TEST dest, src
            Generate(dir, "WAIT
            Generate(dir, "XCHG dest,src
            Generate(dir, "XLAT
            Generate(dir, "XOR dest, src
            Generate(dir, "BOUND
            Generate(dir, "ENTER
            Generate(dir, "INS
            Generate(dir, "INSB
            Generate(dir, "INSW
            Generate(dir, "LEAVE
            Generate(dir, "OUTS
            Generate(dir, "OUTSB
            Generate(dir, "OUTSW
            Generate(dir, "POPA
            Generate(dir, "PUSHA
            Generate(dir, "PUSHW
            */




            JsonTool.Save(dir, "i86.json", res);
        }
    }
}