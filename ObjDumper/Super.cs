using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ObjDumper
{
    internal static class Super
    {
        public static IEnumerable<NumItem> GenerateNumB3()
        {
            for (var a = 0; a < byte.MaxValue; a++)
            for (var b = 0; b < byte.MaxValue; b++)
            for (var c = 0; c < byte.MaxValue; c++)
            {
                byte[] bytes = [(byte)a, (byte)b, (byte)c];
                var hex = ToHex(bytes);
                var i = long.Parse(hex, NumberStyles.HexNumber);
                var itm = new NumItem(i, hex, bytes);
                yield return itm;
            }
        }

        public static IEnumerable<NumItem> GenerateNum16()
        {
            for (long i = 0; i <= ushort.MaxValue; i++)
            {
                var bytes = BitConverter.GetBytes(i);
                var hex = ToHex(bytes);
                var itm = new NumItem(i, hex, bytes);
                yield return itm;
            }
        }

        public static IEnumerable<NumItem> GenerateNum32()
        {
            for (long i = 0; i < uint.MaxValue; i++)
            {
                var bytes = BitConverter.GetBytes(i);
                var hex = ToHex(bytes);
                var itm = new NumItem(i, hex, bytes);
                yield return itm;
            }
        }

        private static string ToHex(byte[] bytes)
        {
            return string.Join("", bytes.Select(b => $"{b:x2}"));
        }

        private static ParsedLine StartAndGet(ProcessStartInfo info)
        {
            info.RedirectStandardOutput = true;
            var proc = Process.Start(info);
            proc!.WaitForExit();
            var stdOut = proc.StandardOutput.ReadToEnd();
            var outLines = stdOut.Split('\n');
            var line1 = outLines.Skip(7).FirstOrDefault();
            var parts = line1!.Split(':', 2);
            var first = parts.Last().Trim();
            if (first.Contains("\tcs") || first.Contains("\tds") ||
                first.Contains("\tes") || first.Contains("\tfs") ||
                first.Contains("\tgs") || first.Contains("\tss") ||
                first.Contains("\taddr32") || first.Contains("\tlock") ||
                first.Contains("\tdata32") || first.Contains("\trepnz") ||
                first.Contains("\trepz") || first.Contains("\txrelease"))
                return null;
            parts = first.Split("  ", 2);
            var hex = parts[0];
            if (parts.Length < 2)
                throw new InvalidOperationException(string.Join(" | ", parts));
            first = parts[1].Trim();
            parts = first.Split(['\t', ' '], 2);
            var cmd = parts[0].Trim();
            var arg = parts.Length == 2 ? parts[1].Trim() : "";
            if (cmd.Equals(".byte") || cmd.Equals(".word") || cmd.Equals("(bad)"))
                return null;
            if (arg.Length == 0)
                arg = null;
            return new ParsedLine(hex, cmd, arg);
        }

        public static void StartSh3(byte[] bytes, IProducerConsumerCollection<ParsedLine> res,
            string dir, long s)
        {
            var name = Path.Combine(dir, $"sh3_{s:D8}.bin");
            File.WriteAllBytes(name, bytes);
            var info = new ProcessStartInfo
            {
                FileName = "sh-elf-objdump",
                ArgumentList = { "-D", "-b", "binary", "-m", "sh3", "-z", name }
            };
            var itm = StartAndGet(info);
            if (itm != null) res.TryAdd(itm);
            File.Delete(name);
        }

        public static void StartI86(byte[] bytes, IProducerConsumerCollection<ParsedLine> res,
            string dir, long s)
        {
            var name = Path.Combine(dir, $"i86_{s:D8}.bin");
            File.WriteAllBytes(name, bytes);
            var info = new ProcessStartInfo
            {
                FileName = "objdump",
                ArgumentList = { "-D", "-Mintel,i8086", "-b", "binary", "-m", "i386", "-z", name },
            };
            var itm = StartAndGet(info);
            if (itm != null) res.TryAdd(itm);
            File.Delete(name);
        }

        public static void Save(IEnumerable<ParsedLine> list, string name, string dir)
        {
            var sort = list.OrderBy(l => l.H.Length)
                .ThenBy(l => l.H).Distinct().ToArray();
            var json = JsonConvert.SerializeObject(sort, new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            });
            var path = Path.Combine(dir, name);
            File.WriteAllText(path, json, Encoding.UTF8);
            Console.WriteLine($"{sort.Length} entries written to {name}!");
        }
    }
}