using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseMovement
{
    class Csv
    {
        public static string Pfad { get; set; } = "pos.csv";
        public static char Separator { get; set; } = ';';
        public static void Save(System.Drawing.Point point)
        {
            using (StreamWriter writer = new StreamWriter(Pfad))
            {
                writer.WriteLine($"{point.X}{Separator}{point.Y}");
            }
        }
        public static void Save(List<System.Drawing.Point> points)
        {
            using (StreamWriter writer = new StreamWriter(Pfad))
            {
                foreach (var point in points)
                {
                    writer.WriteLine($"{point.X}{Separator}{point.Y}");
                }
            }
        }

        public static System.Drawing.Point Read()
        {
            int x = 0;
            int y = 0;
            if (File.Exists(Pfad))
            {
                using (StreamReader reader = new StreamReader(Pfad))
                {
                    string row = reader.ReadLine();
                    string[] fields = row.Split(Separator);
                    x = Convert.ToInt32(fields[0]);
                    y = Convert.ToInt32(fields[1]);
                }
            }
            System.Drawing.Point p = new System.Drawing.Point(x, y);
            return p;
        }
        public static List<System.Drawing.Point> ReadAll()
        {
            int x = 0;
            int y = 0;
            List<System.Drawing.Point> points = new List<System.Drawing.Point>();
            if (File.Exists(Pfad))
            {
                using (StreamReader reader = new StreamReader(Pfad))
                {
                    string row = reader.ReadLine();
                    while (row != null)
                    {
                        string[] fields = row.Split(Separator);
                        x = Convert.ToInt32(fields[0]);
                        y = Convert.ToInt32(fields[1]);
                        System.Drawing.Point p = new System.Drawing.Point(x, y);
                        points.Add(p);
                        row = reader.ReadLine();
                    }
                }
            }
            return points;
        }
    }
}
