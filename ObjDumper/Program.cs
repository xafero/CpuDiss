using System.Linq;

namespace ObjDumper
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var outDir = DirTool.CreateDir("out");

            var x86Gen = new IntelGen(outDir);
            var resI = x86Gen.Generate();
            var x86Allow = x86Gen.CodeNames.ToArray();
            var linI = SqlMan.Create(resI, x86Allow, outDir, "i86.sql");

            var sh3Gen = new HitachiGen(outDir);
            var resH = sh3Gen.Generate();
            var sh3Allow = sh3Gen.CodeNames.ToArray();
            var linH = SqlMan.Create(resH, sh3Allow, outDir, "sh3.sql");

            SqlLite.WriteOut([linI, linH], outDir, "asm.db3");
        }
    }
}