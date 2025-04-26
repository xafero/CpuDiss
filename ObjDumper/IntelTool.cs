using System.IO;
using Iced.Intel;

namespace ObjDumper
{
    public static class IntelTool
    {
        public static Assembler CreateAss()
        {
            return new Assembler(16);
        }

        public static byte[] GetBytes(this Assembler c)
        {
            const ulong ip = 0x0;
            using var stream = new MemoryStream();
            c.Assemble(new StreamCodeWriter(stream), ip);
            return stream.ToArray();
        }
    }
}