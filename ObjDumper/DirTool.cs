using System.IO;

namespace ObjDumper
{
    public static class DirTool
    {
        public static string CreateDir(string name)
        {
            var outDir = Path.GetFullPath(name);
            if (!Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);
            return outDir;
        }
    }
}