using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

            var linesSh3 = Load("sh3.json", outDir);
            var linesI86 = Load("i86.json", outDir);

            var po = new ParallelOptions { MaxDegreeOfParallelism = 6 };
            var stop = new bool[1];

            var sh3T = new Thread(() =>
            {
                // var max = linesSh3.Count == 0 ? 0 : linesSh3.Keys.Max();
                var numbers = IterTool.GenerateNum(2, linesSh3.Keys).ToArray();
                Console.WriteLine($"Starting with {numbers.Length} numbers for sh3!");
                /*
                foreach (var number in numbers)
                {
                    if (stop[0]) continue;
                    StartSh3(number.Bytes, linesSh3, tmpDir, number.Index);
                }
                */
                Parallel.ForEach(numbers, po, number =>
                {
                    if (stop[0]) return;
                    StartSh3(number.Bytes, linesSh3, tmpDir, number.Index);
                });
                Save(linesSh3, "sh3.json", outDir);
            }) { IsBackground = false };
            sh3T.Start();

            var x86T = new Thread(() =>
            {
                // var max = linesI86.Count == 0 ? 0 : linesI86.Keys.Max();
                var numbers = IterTool.GenerateNum(3, linesI86.Keys).ToArray();
                Console.WriteLine($"Starting with {numbers.Length} numbers for i86!");
                /*
                foreach (var number in numbers)
                {
                    if (stop[0]) continue;
                    StartI86(number.Bytes, linesI86, tmpDir, number.Index);
                }
                */
                Parallel.ForEach(numbers, po, number =>
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