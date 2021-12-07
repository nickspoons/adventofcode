using System;
using System.Linq;

namespace AdventOfCode.Advent2021 {
   public class Day07 : AdventDay {
      public override int Day => 7;
      public override int Year => 2021;

      private int[] _crabs;
      private int[] Crabs => _crabs ??
        (_crabs = Input.Split(',').Select(s => int.Parse(s)).ToArray());

      public override string A() {
         int min = Crabs.Min();
         int max = Crabs.Max();
         int smallest = 0;
         for (int depth = min; depth <= max; depth++) {
            int d = Crabs.Sum(c => Math.Abs(depth - c));
            if (smallest == 0 || d < smallest)
               smallest = d;
         }
         return smallest.ToString();
      }

      public override string B() {
         int min = Crabs.Min();
         int max = Crabs.Max();
         int smallest = 0;
         // Optimiztion: pre-calculate depths. This brings running time down
         // from over 2 seconds, to 0.03s
         int[] depths = new int[max + 2];
         for (int d = 1; d < max + 1; d++)
            depths[d] = depths[d - 1] + d;
         for (int depth = min; depth <= max; depth++) {
            int d = Crabs.Sum(c => depths[Math.Abs(depth - c + 1)]);
            if (smallest == 0 || d < smallest)
               smallest = d;
         }
         return smallest.ToString();
      }
   }
}
