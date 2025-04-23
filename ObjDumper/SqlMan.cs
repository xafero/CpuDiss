using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjDumper
{
    internal static class SqlMan
    {
        internal static readonly string[] x86Allow =
        [
            "aaa", "aad", "aam", "aas", "adc", "add", "and", "bound", "call",
            "cbw", "clc", "cld", "cli", "cmc", "cmp", "cmps", "cwd", "daa",
            "das", "dec", "div", "enter", "hlt", "idiv", "imul", "in",
            "inc", "ins", "int", "int1", "int3", "into", "iret", "ja",
            "jae", "jb", "jbe", "jcxz", "je", "jg", "jge", "jl", "jle",
            "jmp", "jne", "jno", "jnp", "jns", "jo", "jp", "js", "lahf",
            "lds", "lea", "leave", "les", "lock", "lods", "loop", "loope",
            "loopne", "mov", "movs", "mul", "neg", "nop", "not", "or",
            "out", "outs", "pop", "popa", "popf", "push", "pusha", "pushf",
            "rcl", "rcr", "rep", "repnz", "repz", "ret", "rol", "ror",
            "sahf", "sar", "sbb", "scas", "shl", "shr", "stc", "std",
            "sti", "stos", "sub", "test", "xchg", "xlat", "xor"
        ];

        internal static readonly string[] sh3Allow =
        [
            "add", "addc", "addv", "and", "and.b", "bf", "bf.s", "bra",
            "braf", "bsr", "bsrf", "bt", "bt.s", "clrmac", "clrs", "clrt",
            "cmp/eq", "cmp/ge", "cmp/gt", "cmp/hi", "cmp/hs", "cmp/pl",
            "cmp/pz", "cmp/str", "div0s", "div0u", "div1", "dmuls.l",
            "dmulu.l", "dt", "exts.b", "exts.w", "extu.b", "extu.w",
            "jmp", "jsr", "ldc", "ldc.l", "lds", "lds.l", "ldtlb",
            "mac.l", "mac.w", "mov", "mov.b", "mov.l", "mov.w", "mova",
            "movt", "mul.l", "muls.w", "mulu.w", "neg", "negc", "nop",
            "not", "or", "or.b", "pref", "rotcl", "rotcr", "rotl", "rotr",
            "rte", "rts", "sets", "sett", "shad", "shal", "shar", "shld",
            "shll", "shll16", "shll2", "shll8", "shlr", "shlr16", "shlr2",
            "shlr8", "sleep", "stc", "stc.l", "sts", "sts.l", "sub",
            "subc", "subv", "swap.b", "swap.w", "tas.b", "trapa",
            "tst", "tst.b", "xor", "xor.b", "xtrct"
        ];

        public static void Create(IDictionary<string, ParsedLine> dict, string[] allowed,
            string dir, string name)
        {
            string[] header =
            [
                "",
                "CREATE TABLE instructions (",
                " id INTEGER, ",
                " hexcode TEXT, ",
                " mnemonic TEXT, ",
                " arguments TEXT ",
                ");",
                ""
            ];
            var groups = dict.Values
                .Where(d => allowed.Contains(d.C))
                .OrderBy(d => d.C)
                .GroupBy(d => d.C)
                .ToArray();
            var stats = new SortedSet<string>();
            foreach (var group in groups)
            {
                foreach (var j in group)
                {
                    var arg = j.A.TrimOrNull() is { } ja ? $"'{ja}'" : "NULL";
                    var sql = "INSERT INTO instructions (mnemonic, hexcode, arguments, id) "
                              + $"VALUES ('{j.C}', '{j.H}', {arg}, {j.I});";
                    stats.Add(sql);
                }
            }
            var lines = new List<string>();
            lines.AddRange(header);
            lines.AddRange(stats);
            Console.WriteLine($"Saving {lines.Count} entries for '{name}'!");
            FileTool.Save(dir, name, lines);
        }
    }
}