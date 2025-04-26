using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ObjDumper
{
    internal static class FileTool
    {
        public static void Save(string dir, string name, IEnumerable<string> lines)
        {
            var path = Path.Combine(dir, name);
            File.WriteAllLines(path, lines, Encoding.UTF8);
        }
    }
}