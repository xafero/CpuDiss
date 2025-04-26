using System;
using System.Collections.Generic;
using System.Linq;
using Iced.Intel;
using static Iced.Intel.AssemblerRegisters;

namespace ObjDumper
{
    public static class IterTool
    {
        public static IEnumerable<ulong> IterUInt64()
        {
            var vals = new[]
            {
                ulong.MinValue, (ulong)128, ulong.MaxValue
            };
            return vals;
        }

        public static IEnumerable<short> IterInt16()
        {
            var vals = new[]
            {
                short.MinValue, short.MaxValue
            };
            return vals;
        }

        public static IEnumerable<ushort> IterUInt16()
        {
            var vals = new[]
            {
                ushort.MinValue, ushort.MaxValue
            };
            return vals;
        }

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

        public static IEnumerable<AssemblerMemoryOperand> IterAmo()
        {
            var mems = new[]
            {
                __byte_ptr[0], __byte_ptr[8], __byte_ptr[16],
                __byte_ptr[32], __byte_ptr[64], __byte_ptr[128],
            };
            return mems;
        }

        public static IEnumerable<AssemblerRegister8> IterAr8()
        {
            var regs = new[]
            {
                ah, al, bh, bl, ch, cl, dh, dl
            };
            return regs;
        }

        public static IEnumerable<AssemblerRegister16> IterAr16()
        {
            var regs = new[]
            {
                ax, bx, cx, dx
            };
            return regs;
        }

        public static object[] IterObject<T>()
        {
            object[] args;
            var type = typeof(T).Name;
            switch (type)
            {
                case "Byte":
                    args = IterBytes().OfType<object>().ToArray(); break;
                case "SByte":
                    args = IterSBytes().OfType<object>().ToArray(); break;
                case "UInt16":
                    args = IterUInt16().OfType<object>().ToArray(); break;
                case "Int16":
                    args = IterInt16().OfType<object>().ToArray(); break;
                case "UInt64":
                    args = IterUInt64().OfType<object>().ToArray(); break;
                case "AssemblerRegister8":
                    args = IterAr8().OfType<object>().ToArray(); break;
                case "AssemblerRegister16":
                    args = IterAr16().OfType<object>().ToArray(); break;
                case "AssemblerMemoryOperand":
                    args = IterAmo().OfType<object>().ToArray(); break;
                default:
                    throw new InvalidOperationException(type);
            }
            return args;
        }
    }
}