using System;
using System.IO;
using System.Linq;

namespace AdventOfCode {
   public abstract class AdventDay : IAdventDay {
      protected abstract int Day { get; }
      protected abstract int Year { get; }

      protected string Input => File.ReadAllText($"Advent{Year}/Day{Day:00}-input");

      protected int[] InputIntLines => InputLines.Select(int.Parse).ToArray();

      protected string[] InputLines => Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

      public abstract string A();
      public abstract string B();
   }
}
