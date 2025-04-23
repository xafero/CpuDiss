using System.IO;

namespace ObjDumper
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var outDir = Directory.CreateDirectory("out").FullName;

            var resI = IntelGen.Generate(outDir);
            SqlMan.Create(resI, SqlMan.x86Allow, outDir, "i86.sql");

            var resH = HitachiGen.Generate(outDir);
            SqlMan.Create(resH, SqlMan.sh3Allow, outDir, "sh3.sql");
        }
    }
}