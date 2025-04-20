using System;
using System.Collections.Generic;

namespace ObjDumper
{
    internal static class IterTool
    {
        public static IEnumerable<NumItem> GenerateNum(int count)
        {
            const int min = 0;
            var max = Math.Pow(2, 8 * count);
            for (long i = min; i <= max; i++)
            {
                var bytes = BitConverter.GetBytes(i);
                // var hex = ToHex(bytes);
                // var id = long.Parse(hex, NumberStyles.HexNumber);
                var itm = new NumItem(i, bytes);
                yield return itm;
            }
        }
    }
}