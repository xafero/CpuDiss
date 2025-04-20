using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static ObjDumper.IterTool;
using static ObjDumper.JsonTool;
using static ObjDumper.ObjdTool;

namespace ObjDumper
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var tmpDir = Directory.CreateDirectory("tmp").FullName;
            var outDir = Directory.CreateDirectory("out").FullName;

            var linesSh3 = new ConcurrentDictionary<long, ParsedLine>();
            var linesI86 = new ConcurrentDictionary<long, ParsedLine>();

            var po = new ParallelOptions { MaxDegreeOfParallelism = 4 };
            var stop = new bool[1];

            var sh3T = new Thread(() =>
            {
                Parallel.ForEach(GenerateNum16(), po, number =>
                {
                    if (stop[0]) return;
                    StartSh3(number.Bytes, linesSh3, tmpDir, number.Index);
                });
                Save(linesSh3, "sh3.json", outDir);
            }) { IsBackground = false };
            sh3T.Start();

            var x86T = new Thread(() =>
            {
                Parallel.ForEach(GenerateNum16(), po, number =>
                {
                    if (stop[0]) return;
                    StartI86(number.Bytes, linesI86, tmpDir, number.Index);
                });
                Save(linesI86, "i86.json", outDir);
            }) { IsBackground = false };
            x86T.Start();

            Console.WriteLine("Processing...");
            Console.ReadLine();

            stop[0] = true;
        }
    }
}