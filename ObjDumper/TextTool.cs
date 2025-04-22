namespace ObjDumper
{
    internal static class TextTool
    {
        public static string TrimOrNull(this string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
        }
    }
}