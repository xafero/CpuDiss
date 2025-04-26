using System.Collections.Generic;

namespace ObjDumper
{
    public static class IterTool
    {
        public static IEnumerable<byte> IterBytes()
        {
            for (int i = byte.MinValue; i < byte.MaxValue + 1; i++)
            {
                var arg = (byte)i;
                yield return arg;
            }
        }

        public static IEnumerable<sbyte> IterSBytes()
        {
            for (int i = sbyte.MinValue; i < sbyte.MaxValue + 1; i++)
            {
                var arg = (sbyte)i;
                yield return arg;
            }
        }
    }
}