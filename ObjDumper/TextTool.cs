using System.Linq;

namespace ObjDumper
{
    internal static class TextTool
    {
        public static string ToHex(byte[] bytes)
        {
            return string.Join("", bytes.Select(b => $"{b:x2}"));
        }
    }
}