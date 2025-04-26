using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace ObjDumper
{
    public static class SqlLite
    {
        public static void WriteOut(ICollection<ICollection<string>> lines, string dir, string name)
        {
            var path = Path.Combine(dir, name);
            if (File.Exists(path))
                File.Delete(path);

            SQLiteConnection.CreateFile(path);

            using var conn = new SQLiteConnection($"Data Source={path};Version=3;");
            conn.Open();

            var count = 0;
            foreach (var pack in lines)
            {
                count += pack.Count;
                var sql = string.Join(Environment.NewLine, pack);
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine($"Writing {count} entries into '{name}'!");
        }
    }
}