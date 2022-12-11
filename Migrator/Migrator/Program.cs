using Autofac;
using Migrator.DI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Migrator.Program
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                IApplication app = scope.Resolve<IApplication>();
                app.Run();
            }
        }
        static void Main2(string[] args)
        {
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                string original = "02:20:26,009 --> 02:20:27,886";
                string fixedTime = FixTime(2, original);
                Console.WriteLine(original);

                Console.WriteLine(fixedTime);

                string path = ConfigurationManager.AppSettings["Path"];
                List<string> file = File.ReadAllLines(path).ToList();
                file.ForEach(f => Console.WriteLine(f));
                int sec = int.Parse(ConfigurationManager.AppSettings["Seconds"]);

                List<string> fileUpdated = new List<string>();

                file.ForEach(f => fileUpdated.Add(FixTime(sec, f)));


                string targetPath = ConfigurationManager.AppSettings["Target"];

                File.WriteAllLines(targetPath, fileUpdated.ToArray());

            }
        }
        private static string FixTime(int seconds, string line)
        {
            string[] parts = line.Split(' ');
            if (parts.Length == 1 || parts[1] != "-->")
                return line;

            IFormatProvider fo;
            TimeSpan newDatetime1 = TimeSpan.Parse(parts[0]) - new TimeSpan(0, 0, seconds);
            TimeSpan newDatetime2 = TimeSpan.Parse(parts[2]) - new TimeSpan(0, 0, seconds);

            return $"{newDatetime1.ToString(@"hh\:mm\:ss\,fff")} --> {newDatetime2.ToString(@"hh\:mm\:ss\,fff")}";
        }
    }
}
