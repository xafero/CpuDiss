using System.IO;

namespace ObjDumper
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var outDir = Directory.CreateDirectory("out").FullName;
            IntelGen.Generate(outDir);
            HitachiGen.Generate(outDir);
        }
    }
}