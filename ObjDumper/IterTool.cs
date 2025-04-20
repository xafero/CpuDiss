using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjDumper
{
    internal static class IterTool
    {
        public static IEnumerable<NumItem> GenerateNum(int count,
            ICollection<long> skip = null, long min = 0)
        {
            var max = Math.Pow(2, 8 * count);
            for (long i = min; i < max; i++)
            {
                if (skip != null && skip.Contains(i))
                    continue;
                var raw = BitConverter.GetBytes(i);
                var bytes = raw.Take(count).ToArray();
                var itm = new NumItem(i, bytes);
                yield return itm;
            }
        }
    }
}