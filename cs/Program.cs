using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode {
   public class Program {
      private static bool ParseArgs(string[] args, out int year, out int day, out bool a, out bool all, out bool input) {
         year = 0;
         day = 0;
         a = true;
         all = false;
         input = false;
         switch (args.Length) {
            case 0:
               return false;
            case 1:
               all = args[0] == "all";
               return all;
            default:
               if (!(int.TryParse(args[0], out year) && int.TryParse(args[1], out day)))
                  return false;
               if (args.Length > 2) {
                  input = args[2] == "input";
                  a = args[2] == "a";
               }
               return true;
         }
      }

      private static void Solve(IAdventDay adventDay, bool a) {
         DateTime start = DateTime.Now;
         string output = a ? adventDay.A() : adventDay.B();
         TimeSpan time = DateTime.Now.Subtract(start);

         string title = $"{adventDay.Year} {adventDay.Day:00}" + (a ? 'A' : 'B');
         Console.WriteLine($"{title} {output,-(80 - 24)}{time}");
      }

      public static void Main(string[] args) {
         if (!ParseArgs(args, out int year, out int day, out bool a, out bool all, out bool input)) {
            Console.WriteLine(@"
Usage:  dotnet run <year> <day> [b]
Usage:  dotnet run all

  eg.   dotnet run 2021 1
        dotnet run 2021 1 b
        dotnet run all

Download the day's input file:
        dotnet run 2021 5 input

Note: downloading the input file requires a valid session cookie, which can be
fetched from browser developer tools.
".Trim());
            return;
         }

         try {
            string name = $"AdventOfCode.Advent{year}.Day{day:00}";
            IEnumerable<Type> adventDayTypes = Assembly.GetCallingAssembly()
               .GetTypes()
               .Where(t => typeof(IAdventDay).IsAssignableFrom(t))
               .Where(t => !t.IsInterface && !t.IsAbstract);

            static IAdventDay activate(Type adventDayType) =>
               (IAdventDay) Activator.CreateInstance(adventDayType);

            if (all)
               foreach (Type adventDayType in adventDayTypes) {
                  Solve(activate(adventDayType), true);
                  Solve(activate(adventDayType), false);
               }
            else {
               Type adventDayType = adventDayTypes.FirstOrDefault(t => t.FullName == name);
               if (adventDayType == null) {
                  Console.WriteLine("Day not solved");
                  return;
               }
               IAdventDay adventDay = activate(adventDayType);
               if (input)
                  adventDay.DownloadInput();
               else
                  Solve(adventDay, a);
            }
         }
         catch (Exception ex) {
            Console.WriteLine($"[{ex.GetType()}]: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
         }
      }
   }
}
