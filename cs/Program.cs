using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode {
   public class Program {
      private static void Solve(Type adventDayType, bool a) {
         IAdventDay adventDay = (IAdventDay) Activator.CreateInstance(adventDayType);

         DateTime start = DateTime.Now;
         string output = a ? adventDay.A() : adventDay.B();
         TimeSpan time = DateTime.Now.Subtract(start);

         string title = $"{adventDay.Year} {adventDay.Day:00}" + (a ? 'A' : 'B');
         Console.WriteLine($"{title} {output,-(80 - 24)}{time}");
      }

      public static void Main(string[] args) {
         int year = 0;
         int day = 0;
         if ((args.Length != 1 || args[0] != "all")
            && (args.Length < 2
               || !int.TryParse(args[0], out year)
               || !int.TryParse(args[1], out day))) {
            Console.WriteLine("Usage:  dotnet.exe run <year> <day> [b]");
            Console.WriteLine("Usage:  dotnet.exe run all");
            Console.WriteLine("                          ");
            Console.WriteLine("  eg.   dotnet.exe run 2019 1");
            Console.WriteLine("        dotnet.exe run 2019 1 b");
            Console.WriteLine("        dotnet.exe run all");
            return;
         }

         try {
            string name = $"AdventOfCode.Advent{year}.Day{day:00}";
            IEnumerable<Type> adventDayTypes = Assembly.GetCallingAssembly()
               .GetTypes()
               .Where(t => typeof(IAdventDay).IsAssignableFrom(t))
               .Where(t => !t.IsInterface && !t.IsAbstract);

            if (args[0] == "all")
               foreach (Type adventDayType in adventDayTypes) {
                  Solve(adventDayType, true);
                  Solve(adventDayType, false);
               }
            else {
               Type adventDayType = adventDayTypes.FirstOrDefault(t => t.FullName == name);
               if (adventDayType == null) {
                  Console.WriteLine("Day not solved");
                  return;
               }
               Solve(adventDayType, args.Length <= 2);
            }
         }
         catch (Exception ex) {
            Console.WriteLine($"[{ex.GetType().ToString()}]: {ex.Message}");
         }
      }
   }
}
