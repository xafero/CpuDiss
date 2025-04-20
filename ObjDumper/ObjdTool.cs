using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static ObjDumper.ProcTool;

namespace ObjDumper
{
    internal static class ObjdTool
    {
        public static void StartSh3(byte[] bytes, IDictionary<string, ParsedLine> res,
            string dir, long s)
        {
            var id = IterTool.GetId(s);
            if (res.ContainsKey(id)) return;
            var name = Path.Combine(dir, $"sh3_{s:D8}.bin");
            File.WriteAllBytes(name, bytes);
            var info = new ProcessStartInfo
            {
                FileName = "sh-elf-objdump",
                ArgumentList = { "-D", "-b", "binary", "-m", "sh3", "-z", name }
            };
            var itm = StartAndGet(info);
            res[id] = itm;
            File.Delete(name);
        }

        public static void StartI86(byte[] bytes, IDictionary<string, ParsedLine> res,
            string dir, long s)
        {
            var id = IterTool.GetId(s);
            if (res.ContainsKey(id)) return;
            var name = Path.Combine(dir, $"i86_{s:D8}.bin");
            File.WriteAllBytes(name, bytes);
            var info = new ProcessStartInfo
            {
                FileName = "objdump",
                ArgumentList = { "-D", "-Mintel,i8086", "-b", "binary", "-m", "i386", "-z", name },
            };
            var itm = StartAndGet(info);
            res[id] = itm;
            File.Delete(name);
        }
    }
}