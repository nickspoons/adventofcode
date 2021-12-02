using System;
using System.IO;
using System.Linq;

namespace AdventOfCode {
   public abstract class AdventDay : IAdventDay {
      protected string Input => File.ReadAllText(InputFilename);
      protected int[] InputIntLines => InputLines.Select(int.Parse).ToArray();
      protected string[] InputLines => Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

      public bool InputExists => File.Exists(InputFilename);
      public string InputFilename => $"Advent{Year}/Day{Day:00}-input";

      public abstract int Day { get; }
      public abstract int Year { get; }

      public abstract string A();
      public abstract string B();
   }
}
