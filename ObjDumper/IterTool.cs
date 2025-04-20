using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ObjDumper
{
    internal static class IterTool
    {
        public static IEnumerable<NumItem> GenerateNum(int count, ICollection<string> skip,
            int limit = 50_000)
        {
            var rnd = new Random();
            for (var i = 0; i < limit; i++)
            {
                var raw = rnd.NextInt64();
                var array = BitConverter.GetBytes(raw);
                var bytes = array.Take(count).ToArray();
                var hex = TextTool.ToHex(bytes);
                var v = long.Parse(hex, NumberStyles.HexNumber);
                var id = GetId(v);
                if (skip != null && skip.Contains(id))
                    continue;
                yield return new NumItem(id, bytes);
            }
        }

        public static IEnumerable<NumItem> GenerateNumN(int count,
            ICollection<string> skip = null, long min = 0)
        {
            var max = (long)Math.Pow(2, 8 * count);
            if (max == (skip?.Count ?? 0))
                yield break;
            for (var j = min; j < max; j++)
            {
                var id = GetId(j);
                if (skip != null && skip.Contains(id))
                    continue;
                var raw = BitConverter.GetBytes(j);
                var bytes = raw.Take(count).ToArray();
                var itm = new NumItem(id, bytes);
                yield return itm;
            }
        }

        public static string GetId(long j)
        {
            return $"{j:D24}";
        }
    }
}