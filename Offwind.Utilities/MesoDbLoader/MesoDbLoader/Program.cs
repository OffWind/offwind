using System;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MesoDbLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            ClearDb();

            LoadMerra();
            LoadFNL();
        }


        private static void ClearDb()
        {
            using (var ctx = new OffwindEntities())
            {
                ctx.MesoscaleTabFiles
                    .ToList()
                    .ForEach(t => ctx.MesoscaleTabFiles.DeleteObject(t));
                ctx.SaveChanges();
            }
            Console.Write("Clear");
            Console.WriteLine();
        }

        private static void LoadMerra()
        {
            string home = ConfigurationManager.AppSettings["merradir"];
            using (var ctx = new OffwindEntities())
            {
                var counter = 0;
                var reader = new StreamReader(Path.Combine(home, "merraseries.cfg"));
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var val = line.Trim().Split("\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (val.Length != 3)
                    {
                        Console.WriteLine("Error: {0}", line);
                        continue;
                    }

                    var dbItem = new MesoscaleTabFile();
                    dbItem.Longitude = ParseDecimal(val[1]); ;
                    dbItem.Latitude = ParseDecimal(val[0]);
                    dbItem.DatabaseId = 2;
                    var path = System.IO.Path.Combine(home, String.Format("50mMERRAnear_{0}.dat.tab", val[2]));

                    using (var sr = new StreamReader(path))
                    {
                        dbItem.Text = sr.ReadToEnd();
                    }

                    ctx.MesoscaleTabFiles.AddObject(dbItem);

                    counter++;

                    if (counter > 100)
                    {
                        Console.Write("Writing... ");
                        ctx.SaveChanges();
                        Console.Write("OK");
                        Console.WriteLine();
                        counter = 0;
                    }
                }
                ctx.SaveChanges();
            }
            
        }


        private static void LoadFNL()
        {
            string home = ConfigurationManager.AppSettings["fnldir"];
            using (var ctx = new OffwindEntities())
            {
                var counter = 0;
                foreach (var d in Directory.EnumerateFiles(home, "*.dat.tab", SearchOption.TopDirectoryOnly))
                {
                    var filename = System.IO.Path.GetFileName(d);
                    //Console.WriteLine(filename);
                    var f = filename.Replace(".dat.tab", "");
                    var parts = f.Split('_');

                    var longitude = ParseDecimal(parts[0].TrimEnd("NESW".ToCharArray()));
                    var latitude = ParseDecimal(parts[1].TrimEnd("NESW".ToCharArray()));
                    if (parts[0].EndsWith("W")) longitude = -longitude;
                    if (parts[1].EndsWith("S")) latitude = -latitude;

                    var dbItem = new MesoscaleTabFile();
                    dbItem.Longitude = longitude;
                    dbItem.Latitude = latitude;
                    dbItem.DatabaseId = 1;
                    var path = System.IO.Path.Combine(home, filename);

                    using (var sr = new StreamReader(path))
                    {
                        dbItem.Text = sr.ReadToEnd();
                    }

                    ctx.MesoscaleTabFiles.AddObject(dbItem);

                    counter++;

                    if (counter > 100)
                    {
                        Console.Write("Writing... ");
                        ctx.SaveChanges();
                        Console.Write("OK");
                        Console.WriteLine();
                        counter = 0;
                    }
                }
                ctx.SaveChanges();
            }
        }

        private static decimal ParseDecimal(string input)
        {
            decimal dr;
            if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out dr))
                return dr;
            return 0;
        }
    }
}
