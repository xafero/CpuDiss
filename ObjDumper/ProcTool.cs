using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace ObjDumper
{
    public static class ProcTool
    {
        public static ParsedLine StartAndGet(this ProcessStartInfo info)
        {
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            var proc = Process.Start(info);
            proc!.WaitForExit();
            var stdOut = proc.StandardOutput.ReadToEnd();
            var stdErr = proc.StandardError.ReadToEnd();
            var std = $"{stdOut}\n{stdErr}".TrimOrNull();
            var lines = std.Split('\n');
            var line1 = lines.Skip(6).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(line1))
                return null;
            var parts = line1!.Split(':', 2);
            var rawLine = parts[1].TrimOrNull();
            var rParts = rawLine.Split(["  ", " \t"], 2, StringSplitOptions.None);
            var rAddr = rParts[0].TrimOrNull();
            var rText = rParts[1].TrimOrNull();
            var cParts = rText.Split([" ", "\t"], 2,
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var cCmd = cParts[0].TrimOrNull();
            var cArg = cParts.Length >= 2 ? cParts[1].TrimOrNull() : string.Empty;
            var tmp = "\t! 0";
            if (cArg.EndsWith(tmp)) cArg = cArg.Replace(tmp, string.Empty);
            var tightHex = rAddr.Replace(" ", "");
            var idx = long.Parse(tightHex, NumberStyles.HexNumber);
            return new ParsedLine(idx, rAddr, cCmd, cArg.TrimOrNull());
        }
    }
}