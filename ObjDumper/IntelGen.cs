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
            var bytes = c.GetBytes();
            var nr = Convert.ToHexString(bytes);
            StartI86(bytes, res, dir, nr);
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
            const string name = "i86.json";
            var res = JsonTool.Load<SortedDictionary<string, ParsedLine>>(dir, name);
            Console.WriteLine($"Loading {res.Count} entries from '{name}'!");

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
            Generate(dir, "cmpsb", res, (c) => c.cmpsb());
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
            Generate(dir, "jae", res, c => c.jae(6));
            Generate(dir, "jnb", res, c => c.jnb(5));
            Generate(dir, "jb", res, c => c.jb(4));
            Generate(dir, "jnae", res, c => c.jnae(3));
            Generate(dir, "jbe", res, c => c.jbe(2));
            Generate(dir, "jna", res, c => c.jna(1));
            Generate(dir, "jc", res, c => c.jc(2));
            Generate(dir, "jcxz", res, c => c.jcxz(3));
            Generate(dir, "je", res, c => c.je(2));
            Generate(dir, "jz", res, c => c.jz(7));
            Generate(dir, "jg", res, c => c.jg(8));
            Generate(dir, "jnle", res, c => c.jnle(3));
            Generate(dir, "jge", res, c => c.jge(1));
            Generate(dir, "jnl", res, c => c.jnl(2));
            Generate(dir, "jle", res, c => c.jle(3));
            Generate(dir, "jng", res, c => c.jng(2));
            Generate(dir, "jl", res, c => c.jl(4));
            Generate(dir, "jnge", res, c => c.jnge(3));
            Generate(dir, "jmp", res, c => c.jmp(2));
            Generate(dir, "jnc", res, c => c.jnc(7));
            Generate(dir, "jne", res, c => c.jne(8));
            Generate(dir, "jnz", res, c => c.jnz(8));
            Generate(dir, "jno", res, c => c.jno(6));
            Generate(dir, "jnp", res, c => c.jnp(5));
            Generate(dir, "jpo", res, c => c.jpo(4));
            Generate(dir, "jns", res, c => c.jns(3));
            Generate(dir, "jo", res, c => c.jo(2));
            Generate(dir, "jp", res, c => c.jp(1));
            Generate(dir, "jpe", res, c => c.jpe(4));
            Generate(dir, "js", res, c => c.js(5));
            Generate(dir, "lahf", res, c => c.lahf());
            // Generate16(dir, "lds", res, (c, a) => c.lds(a, NewMo()));
            // Generate16(dir, "lea", res, (c, a) => c.lea(a, new AssemblerMemoryOperand()));
            // Generate16(dir, "les", res, (c, a) => c.les(a, new AssemblerMemoryOperand()));
            Generate(dir, "lodsb", res, c => c.lodsb());
            Generate(dir, "lodsw", res, c => c.lodsw());
            Generate(dir, "loop", res, c => c.loop(38));
            Generate(dir, "loopnz", res, c => c.loopnz(41));
            Generate(dir, "loopz", res, (c) => c.loopz(39));
            Generate16(dir, "mov", res, (c, a, b) => c.mov(a, b));
            Generate(dir, "movsb", res, c => c.movsb());
            Generate(dir, "movsw", res, c => c.movsw());
            Generate16(dir, "mul", res, (c, a) => c.mul(a));
            Generate16(dir, "neg", res, (c, a) => c.neg(a));
            Generate(dir, "nop", res, c => c.nop());
            Generate16(dir, "not", res, (c, a) => c.not(a));
            Generate16(dir, "or", res, (c, a, b) => c.or(a, b));
            Generate16(dir, "out", res, (c, a) => c.@out(8, new AssemblerRegister16(Register.AX)));
            Generate16(dir, "pop", res, (c, a) => c.pop(a));
            Generate(dir, "popf", res, c => c.popf());
            Generate16(dir, "push", res, (c, a) => c.push(a));
            Generate(dir, "pushf", res, c => c.pushf());
            Generate16(dir, "rcl", res, (c, a, b) => c.rcl(new AssemblerRegister8(Register.AH), new AssemblerRegister8(Register.CL)));
            Generate16(dir, "rcr", res, (c, a, b) => c.rcr(new AssemblerRegister8(Register.AH), new AssemblerRegister8(Register.CL)));
            Generate(dir, "ret", res, c => c.ret());
            Generate16(dir, "rol", res, (c, a, b) => c.rol(new AssemblerRegister8(Register.AH), new AssemblerRegister8(Register.CL)));
            Generate16(dir, "ror", res, (c, a, b) => c.ror(new AssemblerRegister8(Register.AH), new AssemblerRegister8(Register.CL)));
            Generate(dir, "sahf", res, c => c.sahf());
            Generate16(dir, "sal", res, (c, a, b) => c.sal(new AssemblerRegister8(Register.AH), new AssemblerRegister8(Register.CL)));
            Generate16(dir, "sar", res, (c, a, b) => c.sar(new AssemblerRegister8(Register.AH), new AssemblerRegister8(Register.CL)));
            Generate16(dir, "sbb", res, (c, a, b) => c.sbb(a, b));
            Generate(dir, "scasb", res, c => c.scasb());
            Generate(dir, "scasw", res, c => c.scasw());
            Generate16(dir, "shl", res, (c, a, b) => c.shl(new AssemblerRegister8(Register.AH), new AssemblerRegister8(Register.CL)));
            Generate16(dir, "shr", res, (c, a, b) => c.shr(new AssemblerRegister8(Register.AH), new AssemblerRegister8(Register.CL)));
            Generate(dir, "stc", res, c => c.stc());
            Generate(dir, "std", res, c => c.std());
            Generate(dir, "sti", res, c => c.sti());
            Generate(dir, "stosb", res, c => c.stosb());
            Generate(dir, "stosw", res, c => c.stosw());
            Generate16(dir, "sub", res, (c, a, b) => c.sub(a, b));
            Generate16(dir, "test", res, (c, a, b) => c.test(a, b));
            Generate(dir, "wait", res, c => c.wait());
            Generate16(dir, "xchg", res, (c, a, b) => c.xchg(a, b));
            Generate(dir, "xlatb", res, c => c.xlatb());
            Generate(dir, "xor", res, c => c.xor(new AssemblerRegister16(Register.AX), 2));
            // Generate(dir, "bound", res, c => c.bound(new AssemblerRegister16(Register.AX), new AssemblerMemoryOperand()));
            Generate(dir, "enter", res, c => c.enter(1, 2));
            Generate(dir, "insb", res, c => c.insb());
            Generate(dir, "insw", res, c => c.insw());
            Generate(dir, "leave", res, c => c.leave());
            Generate(dir, "outsb", res, c => c.outsb());
            Generate(dir, "outsw", res, c => c.outsw());
            Generate(dir, "popa", res, c => c.popa());
            Generate(dir, "pusha", res, c => c.pusha());
            
            Console.WriteLine($"Saving {res.Count} entries for '{name}'!");
            JsonTool.Save(dir, name, res);
        }
    }
}