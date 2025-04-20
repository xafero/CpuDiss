using System;
using System.Diagnostics;
using System.Linq;

namespace ObjDumper
{
    internal static class ProcTool
    {
        public static ParsedLine StartAndGet(ProcessStartInfo info)
        {
            info.RedirectStandardOutput = true;
            var proc = Process.Start(info);
            proc!.WaitForExit();
            var stdOut = proc.StandardOutput.ReadToEnd();
            var outLines = stdOut.Split('\n');
            var line1 = outLines.Skip(7).FirstOrDefault();
            var parts = line1!.Split(':', 2);
            var first = parts.Last().Trim();
            if (first.Contains("\tcs") || first.Contains("\tds") ||
                first.Contains("\tes") || first.Contains("\tfs") ||
                first.Contains("\tgs") || first.Contains("\tss") ||
                first.Contains("\taddr32") || first.Contains("\tlock") ||
                first.Contains("\tdata32") || first.Contains("\trepnz") ||
                first.Contains("\trepz") || first.Contains("\txrelease"))
                return null;
            parts = first.Split("  ", 2);
            var hex = parts[0];
            if (parts.Length < 2)
                throw new InvalidOperationException(string.Join(" | ", parts));
            first = parts[1].Trim();
            parts = first.Split(['\t', ' '], 2);
            var cmd = parts[0].Trim();
            var arg = parts.Length == 2 ? parts[1].Trim() : "";
            if (cmd.Equals(".byte") || cmd.Equals(".word") || cmd.Equals("(bad)"))
                return null;
            if (arg.Length == 0)
                arg = null;
            return new ParsedLine(hex, cmd, arg);
        }
    }
}