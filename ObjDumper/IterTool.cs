using System;
using System.Collections.Generic;
using System.Globalization;
using static ObjDumper.TextTool;

namespace ObjDumper
{
    internal static class IterTool
    {
        public static IEnumerable<NumItem> GenerateNumB3()
        {
            for (var a = 0; a < byte.MaxValue; a++)
            for (var b = 0; b < byte.MaxValue; b++)
            for (var c = 0; c < byte.MaxValue; c++)
            {
                byte[] bytes = [(byte)a, (byte)b, (byte)c];
                var hex = ToHex(bytes);
                var i = long.Parse(hex, NumberStyles.HexNumber);
                var itm = new NumItem(i, hex, bytes);
                yield return itm;
            }
        }

        public static IEnumerable<NumItem> GenerateNum16()
        {
            for (long i = 0; i <= byte.MaxValue; i++)
            {
                var bytes = BitConverter.GetBytes(i);
                var hex = ToHex(bytes);
                var itm = new NumItem(i, hex, bytes);
                yield return itm;
            }
        }

        public static IEnumerable<NumItem> GenerateNum32()
        {
            for (long i = 0; i < uint.MaxValue; i++)
            {
                var bytes = BitConverter.GetBytes(i);
                var hex = ToHex(bytes);
                var itm = new NumItem(i, hex, bytes);
                yield return itm;
            }
        }
    }
}