using System;
using System.Collections.Generic;
using System.Linq;
using ObjDumper.Core;
using System.Diagnostics;
using System.IO;
using Iced.Intel;
using static ObjDumper.IterTool;
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

            //for (var i = 0; i < ushort.MaxValue + 1; i++)
              //  Generate(i, res);

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

        private void Do<T1>(D res, Action<Assembler, T1> action)
        {
            object[] args;
            var type = typeof(T1).Name;
            switch (type)
            {
                case "Byte": args = IterBytes().OfType<object>().ToArray(); break;
                case "SByte": args = IterSBytes().OfType<object>().ToArray(); break;
                default: throw new InvalidOperationException(type);
            }
            foreach (var arg in args)
                Do(res, a => action.DynamicInvoke(a, arg));
        }

        private void GenerateExtra(D r)
        {
            Do(r, a => a.aaa());
            Do<byte>(r, (a, x) => a.aad(x));
            Do<sbyte>(r, (a, x) => a.aad(x));
            Do<byte>(r, (a,x) => a.aam(x));
            Do<sbyte>(r, (a,x) => a.aam(x));
            Do(r, a => a.aas());
            
            
            
            
            // Do(r, a => a.adc());
            // Do(r, a => a.add());
            // Do(r, a => a.and());
            // Do(r, a => a.bound());
            // Do(r, a => a.call());
            // Do(r, a => a.cbw());
            // Do(r, a => a.clc());
            // Do(r, a => a.cld());
            // Do(r, a => a.cli());
            // Do(r, a => a.cmc());
            // Do(r, a => a.cmp());
            // Do(r, a => a.cmpsb());
            // Do(r, a => a.cmpsw());
            // Do(r, a => a.cwd());
            // Do(r, a => a.daa());
            // Do(r, a => a.das());
            // Do(r, a => a.dec());
            // Do(r, a => a.div());
            // Do(r, a => a.enter());
            // Do(r, a => a.hlt());
            // Do(r, a => a.idiv());
            // Do(r, a => a.imul());
            // Do(r, a => a.@in());
            // Do(r, a => a.inc());
            // Do(r, a => a.insb());
            // Do(r, a => a.insw());
            // Do(r, a => a.@int());
            // Do(r, a => a.int1());
            // Do(r, a => a.int3());
            // Do(r, a => a.into());
            // Do(r, a => a.iret());
            // Do(r, a => a.ja());
            // Do(r, a => a.jae());
            // Do(r, a => a.jb());
            // Do(r, a => a.jbe());
            // Do(r, a => a.jc());
            // Do(r, a => a.jcxz());
            // Do(r, a => a.je());
            // Do(r, a => a.jg());
            // Do(r, a => a.jge());
            // Do(r, a => a.jl());
            // Do(r, a => a.jle());
            // Do(r, a => a.jmp());
            // Do(r, a => a.jna());
            // Do(r, a => a.jnae());
            // Do(r, a => a.jnb());
            // Do(r, a => a.jnbe());
            // Do(r, a => a.jnc());
            // Do(r, a => a.jne());
            // Do(r, a => a.jng());
            // Do(r, a => a.jnge());
            // Do(r, a => a.jnl());
            // Do(r, a => a.jnle());
            // Do(r, a => a.jno());
            // Do(r, a => a.jnp());
            // Do(r, a => a.jns());
            // Do(r, a => a.jnz());
            // Do(r, a => a.jo());
            // Do(r, a => a.jp());
            // Do(r, a => a.jpe());
            // Do(r, a => a.jpo());
            // Do(r, a => a.js());
            // Do(r, a => a.jz());
            // Do(r, a => a.lahf());
            // Do(r, a => a.lds());
            // Do(r, a => a.lea());
            // Do(r, a => a.leave());
            // Do(r, a => a.les());
            // Do(r, a => a.@lock());
            // Do(r, a => a.lodsb());
            // Do(r, a => a.lodsw());
            // Do(r, a => a.loop());
            // Do(r, a => a.loope());
            // Do(r, a => a.loopne());
            // Do(r, a => a.loopnz());
            // Do(r, a => a.loopz());
            // Do(r, a => a.mov());
            // Do(r, a => a.movsb());
            // Do(r, a => a.movsw());
            // Do(r, a => a.mul());
            // Do(r, a => a.neg());
            // Do(r, a => a.nop());
            // Do(r, a => a.not());
            // Do(r, a => a.or());
            // Do(r, a => a.@out());
            // Do(r, a => a.outsb());
            // Do(r, a => a.outsw());
            // Do(r, a => a.pop());
            // Do(r, a => a.popa());
            // Do(r, a => a.popf());
            // Do(r, a => a.push());
            // Do(r, a => a.pusha());
            // Do(r, a => a.pushf());
            // Do(r, a => a.rcl());
            // Do(r, a => a.rcr());
            // Do(r, a => a.rep());
            // Do(r, a => a.repe());
            // Do(r, a => a.repne());
            // Do(r, a => a.repnz());
            // Do(r, a => a.repz());
            // Do(r, a => a.ret());
            // Do(r, a => a.retf());
            // Do(r, a => a.rol());
            // Do(r, a => a.ror());
            // Do(r, a => a.sahf());
            // Do(r, a => a.sal());
            // Do(r, a => a.sar());
            // Do(r, a => a.sbb());
            // Do(r, a => a.scasb());
            // Do(r, a => a.scasw());
            // Do(r, a => a.shl());
            // Do(r, a => a.shr());
            // Do(r, a => a.stc());
            // Do(r, a => a.std());
            // Do(r, a => a.sti());
            // Do(r, a => a.stosb());
            // Do(r, a => a.stosw());
            // Do(r, a => a.sub());
            // Do(r, a => a.test());
            // Do(r, a => a.wait());
            // Do(r, a => a.xchg());
            // Do(r, a => a.xlatb());
            // Do(r, a => a.xor());
        }
    }
}