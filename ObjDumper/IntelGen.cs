using System;
using System.Collections.Generic;
using System.Linq;
using ObjDumper.Core;

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
    }
}