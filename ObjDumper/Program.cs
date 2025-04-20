using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using static ObjDumper.Super;

namespace ObjDumper
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var tmpDir = Directory.CreateDirectory("tmp").FullName;
            var outDir = Directory.CreateDirectory("out").FullName;

            var linesSh3 = new ConcurrentBag<ParsedLine>();
            Parallel.ForEach(GenerateNum16(), number =>
            {
                // Execute SH3
                StartSh3(number.Bytes, linesSh3, tmpDir, number.Index);
            });
            Save(linesSh3, "sh3.json", outDir);

            var linesI86 = new ConcurrentBag<ParsedLine>();
            Parallel.ForEach(GenerateNum16(), number =>
            {
                // Execute x86
                StartI86(number.Bytes, linesI86, tmpDir, number.Index);
            });
            Save(linesI86, "i86.json", outDir);
        }
    }
}