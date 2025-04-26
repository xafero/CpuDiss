using System;
using System.Collections.Generic;
using System.Linq;
using ObjDumper.Core;

namespace ObjDumper
{
    public sealed class HitachiGen
    {
        public HitachiGen(string outDir)
        {
            OutDir = outDir;
        }

        public string OutDir { get; }

        public IEnumerable<Sh3> Codes => Enum.GetValues<Sh3>().Except([Sh3.None]);
    }
}