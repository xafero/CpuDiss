using System;
using System.Collections.Generic;
using System.Linq;
using ObjDumper.Core;
using System.Diagnostics;
using System.IO;
using Iced.Intel;
using static ObjDumper.IterTool;
using D = System.Collections.Generic.IDictionary<string, ObjDumper.ParsedLine>;
using A8  = Iced.Intel.AssemblerRegister8;
using A16 = Iced.Intel.AssemblerRegister16;
using AM  = Iced.Intel.AssemblerMemoryOperand;

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
            if (bytes == null) return;
            var nr = Convert.ToHexString(bytes);
            StartI86(bytes, res, OutDir, nr);
        }

        private void Do(D res, Action<Assembler> action)
        {
            var asm = IntelTool.CreateAss();
            action(asm);
            Generate(asm.GetBytes(), res);
        }

        private void Do<T1>(D res, Action<Assembler, T1> action)
        {
            foreach (var arg in IterObject<T1>())
                Do(res, a => action.DynamicInvoke(a, arg));
        }

        private void Do<T1, T2>(D res, Action<Assembler, T1, T2> action)
        {
            foreach (var arg1 in IterObject<T1>())
            foreach (var arg2 in IterObject<T2>())
                Do(res, a => action.DynamicInvoke(a, arg1, arg2));
        }

        private void GenerateExtra(D r)
        {
            Do(r, a => a.aaa());
            Do<byte>(r, (a, x) => a.aad(x));
            Do<sbyte>(r, (a, x) => a.aad(x));
            Do<byte>(r, (a, x) => a.aam(x));
            Do<sbyte>(r, (a, x) => a.aam(x));
            Do(r, a => a.aas());
            Do<A8, A8>(r, (a, x, y) => a.adc(x, y));
            Do<A8, A8>(r, (a, x, y) => a.add(x, y));
            Do<A8, A8>(r, (a, x, y) => a.and(x, y));
            Do<A16, AM>(r, (a, x, y) => a.bound(x, y));
            Do<A16>(r, (a, x) => a.call(x));
            Do(r, a => a.cbw());
            Do(r, a => a.clc());
            Do(r, a => a.cld());
            Do(r, a => a.cli());
            Do(r, a => a.cmc());
            Do<A8, byte>(r, (a, x, y) => a.cmp(x, y));
            Do<A8, sbyte>(r, (a, x, y) => a.cmp(x, y));
            Do(r, a => a.cmpsb());
            Do(r, a => a.cmpsw());
            Do(r, a => a.cwd());
            Do(r, a => a.daa());
            Do(r, a => a.das());
            Do<A8>(r, (a, x) => a.dec(x));
            Do<A8>(r, (a, x) => a.div(x));
            Do<short, sbyte>(r, (a, x, y) => a.enter(x, y));
            Do<ushort, byte>(r, (a, x, y) => a.enter(x, y));
            Do(r, a => a.hlt());
            Do<A8>(r, (a, x) => a.idiv(x));
            Do<A8>(r, (a, x) => a.imul(x));
            Do<A8, byte>(r, (a, x, y) => a.@in(x, y));
            Do<A8, sbyte>(r, (a, x, y) => a.@in(x, y));
            Do<A8>(r, (a, x) => a.inc(x));
            Do(r, a => a.insb());
            Do(r, a => a.insw());
            Do<byte>(r, (a, x) => a.@int(x));
            Do<sbyte>(r, (a, x) => a.@int(x));
            Do(r, a => a.int1());
            Do(r, a => a.int3());
            Do(r, a => a.into());
            Do(r, a => a.iret());
            Do<ulong>(r, (a, x) => a.ja(x));
            Do<ulong>(r, (a, x) => a.jae(x));
            Do<ulong>(r, (a, x) => a.jb(x));
            Do<ulong>(r, (a, x) => a.jbe(x));
            Do<ulong>(r, (a, x) => a.jc(x));
            Do<ulong>(r, (a, x) => a.jcxz(x));
            Do<ulong>(r, (a, x) => a.je(x));
            Do<ulong>(r, (a, x) => a.jg(x));
            Do<ulong>(r, (a, x) => a.jge(x));
            Do<ulong>(r, (a, x) => a.jl(x));
            Do<ulong>(r, (a, x) => a.jle(x));
            Do<ulong>(r, (a, x) => a.jmp(x));
            Do<ulong>(r, (a, x) => a.jna(x));
            Do<ulong>(r, (a, x) => a.jnae(x));
            Do<ulong>(r, (a, x) => a.jnb(x));
            Do<ulong>(r, (a, x) => a.jnbe(x));
            Do<ulong>(r, (a, x) => a.jnc(x));
            Do<ulong>(r, (a, x) => a.jne(x));
            Do<ulong>(r, (a, x) => a.jng(x));
            Do<ulong>(r, (a, x) => a.jnge(x));
            Do<ulong>(r, (a, x) => a.jnl(x));
            Do<ulong>(r, (a, x) => a.jnle(x));
            Do<ulong>(r, (a, x) => a.jno(x));
            Do<ulong>(r, (a, x) => a.jnp(x));
            Do<ulong>(r, (a, x) => a.jns(x));
            Do<ulong>(r, (a, x) => a.jnz(x));
            Do<ulong>(r, (a, x) => a.jo(x));
            Do<ulong>(r, (a, x) => a.jp(x));
            Do<ulong>(r, (a, x) => a.jpe(x));
            Do<ulong>(r, (a, x) => a.jpo(x));
            Do<ulong>(r, (a, x) => a.js(x));
            Do<ulong>(r, (a, x) => a.jz(x));
            Do(r, a => a.lahf());
            Do<A16, AM>(r, (a, x, y) => a.lds(x, y));
            Do<A16, AM>(r, (a, x, y) => a.lea(x, y));
            Do(r, a => a.leave());
            Do<A16, AM>(r, (a, x, y) => a.les(x, y));
            Do<A16>(r, (a, x) => a.@lock.inc(x));
            Do(r, a => a.lodsb());
            Do(r, a => a.lodsw());
            Do<ulong>(r, (a, x) => a.loop(x));
            Do<ulong>(r, (a, x) => a.loope(x));
            Do<ulong>(r, (a, x) => a.loopne(x));
            Do<ulong>(r, (a, x) => a.loopnz(x));
            Do<ulong>(r, (a, x) => a.loopz(x));
            Do<A8, A8>(r, (a, x, y) => a.mov(x, y));
            Do(r, a => a.movsb());
            Do(r, a => a.movsw());
            Do<A8>(r, (a, x) => a.mul(x));
            Do<A16>(r, (a, x) => a.mul(x));
            Do<A8>(r, (a, x) => a.neg(x));
            Do(r, a => a.nop());
            Do<A8>(r, (a, x) => a.not(x));
            Do<A8, sbyte>(r, (a, x, y) => a.or(x, y));
            Do<A16, A8>(r, (a, x, y) => a.@out(x, y));
            Do<byte, A8>(r, (a, x, y) => a.@out(x, y));
            Do(r, a => a.outsb());
            Do(r, a => a.outsw());
            Do<A16>(r, (a, x) => a.pop(x));
            Do(r, a => a.popa());
            Do(r, a => a.popf());
            Do<A16>(r, (a, x) => a.push(x));
            Do(r, a => a.pusha());
            Do(r, a => a.pushf());
            Do<A8, byte>(r, (a, x, y) => a.rcl(x, y));
            Do<A8, byte>(r, (a, x, y) => a.rcr(x, y));
            Do(r, a => a.rep.movsb());
            Do(r, a => a.repe.cmpsb());
            Do(r, a => a.repne.scasb());
            Do(r, a => a.repnz.movsb());
            Do(r, a => a.repz.cmpsb());
            Do(r, a => a.ret());
            Do(r, a => a.retf());
            Do<A8, byte>(r, (a, x, y) => a.rol(x, y));
            Do<A8, byte>(r, (a, x, y) => a.ror(x, y));
            Do(r, a => a.sahf());
            Do<A8, byte>(r, (a, x, y) => a.sal(x, y));
            Do<A8, byte>(r, (a, x, y) => a.sar(x, y));
            Do<A8, sbyte>(r, (a, x, y) => a.sbb(x, y));
            Do(r, a => a.scasb());
            Do(r, a => a.scasw());
            Do<A8, byte>(r, (a, x, y) => a.shl(x, y));
            Do<A8, byte>(r, (a, x, y) => a.shr(x, y));
            Do(r, a => a.stc());
            Do(r, a => a.std());
            Do(r, a => a.sti());
            Do(r, a => a.stosb());
            Do(r, a => a.stosw());
            Do<A8, sbyte>(r, (a, x, y) => a.sub(x, y));
            Do<A8, byte>(r, (a, x, y) => a.test(x, y));
            Do(r, a => a.wait());
            Do<A8, A8>(r, (a, x, y) => a.xchg(x, y));
            Do(r, a => a.xlatb());
            Do<A8, sbyte>(r, (a, x, y) => a.xor(x, y));
        }
    }
}