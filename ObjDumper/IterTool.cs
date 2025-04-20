using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjDumper
{
    internal static class IterTool
    {
        public static IEnumerable<NumItem> GenerateNum(int count,
            ICollection<string> skip = null, long min = 0)
        {
            var max = Math.Pow(2, 8 * count);
            for (long j = min; j < max; j++)
            {
                var id = GetId(j);
                if (skip != null && skip.Contains(id))
                    continue;
                var raw = BitConverter.GetBytes(j);
                var bytes = raw.Take(count).ToArray();
                var itm = new NumItem(j, bytes);
                yield return itm;
            }
        }

        public static string GetId(long j)
        {
            return $"{j:D24}";
        }
    }
}