using System.Linq;

namespace ObjDumper
{
    internal static class TextTool
    {
        public static string ToHex(byte[] bytes)
        {
            return string.Join("", bytes.Select(b => $"{b:x2}"));
        }

        public static string TrimOrNull(this string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
        }
    }
}