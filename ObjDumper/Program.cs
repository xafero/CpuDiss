using System;

namespace ObjDumper
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var outDir = DirTool.CreateDir("out");

            var x86Gen = new IntelGen(outDir);
            Console.WriteLine(" * " + string.Join(", ", x86Gen.Codes));
            Console.WriteLine(" * " + string.Join(", ", x86Gen.CodeNames));
            x86Gen.Generate();

            Console.WriteLine();

            // var sh3Gen = new HitachiGen(outDir);
            // Console.WriteLine(" * " + string.Join(", ", sh3Gen.Codes));
            // Console.WriteLine(" * " + string.Join(", ", sh3Gen.CodeNames));
            // sh3Gen.Generate();

        }
    }
}
