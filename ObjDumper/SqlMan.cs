using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ObjDumper
{
    internal static class SqlMan
    {
        public static List<string> Create(IDictionary<string, ParsedLine> dict, string[] allowed,
            string dir, string name)
        {
            var lbl = Path.GetFileNameWithoutExtension(name).ToUpper();
            var table = $"instructions{lbl}";
            string[] header =
            [
                "",
                $"CREATE TABLE {table} (",
                " id INTEGER, ",
                " hexcode TEXT, ",
                " mnemonic TEXT, ",
                " arguments TEXT ",
                ");",
                "",
                $"INSERT INTO {table} (mnemonic, hexcode, arguments, id) ",
                "VALUES"
            ];
            var groups = dict.Values
                .Where(d => allowed.Contains(d.C))
                .OrderBy(d => d.C)
                .GroupBy(d => d.C)
                .ToArray();
            var stats = new SortedSet<string>();
            foreach (var group in groups)
            {
                foreach (var j in group)
                {
                    var arg = j.A.TrimOrNull() is { } ja ? $"'{ja}'" : "NULL";
                    var sql = $"  ('{j.C}', '{j.H}', {arg}, {j.I}),";
                    stats.Add(sql);
                }
            }
            var lines = new List<string>();
            lines.AddRange(header);
            lines.AddRange(stats);
            lines[^1] = lines[^1].Replace("),", ");");

            Console.WriteLine($"Saving {lines.Count} entries for '{name}'!");
            FileTool.Save(dir, name, lines);

            return lines;
        }
    }
}