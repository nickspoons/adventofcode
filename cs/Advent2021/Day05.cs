using System.Linq;

namespace AdventOfCode.Advent2021 {
   public class Day05 : AdventDay {
      public override int Day => 5;
      public override int Year => 2021;

      public override string A() {
         int[,] grid = new int[1000, 1000];
         foreach (string line in InputLines) {
            string[] parts = line.Split(" -> ", 2);
            int[] from = parts[0].Split(",", 2).Select(n => int.Parse(n)).ToArray();
            int[] to = parts[1].Split(",", 2).Select(n => int.Parse(n)).ToArray();
            int stepx = to[0] > from[0] ? 1 : from[0] == to[0] ? 0 : -1;
            int stepy = to[1] > from[1] ? 1 : from[1] == to[1] ? 0 : -1;
            if (!(stepx == 0 || stepy == 0))
               continue;
            while (from[0] != to[0] || from[1] != to[1]) {
               grid[from[0], from[1]] += 1;
               from[0] += stepx;
               from[1] += stepy;
            }
            grid[from[0], from[1]] += 1;
         }
         return grid.Cast<int>().Count(n => n > 1).ToString();
      }

      public override string B() {
         int[,] grid = new int[1000, 1000];
         foreach (string line in InputLines) {
            string[] parts = line.Split(" -> ", 2);
            int[] from = parts[0].Split(",", 2).Select(n => int.Parse(n)).ToArray();
            int[] to = parts[1].Split(",", 2).Select(n => int.Parse(n)).ToArray();
            int stepx = to[0] > from[0] ? 1 : from[0] == to[0] ? 0 : -1;
            int stepy = to[1] > from[1] ? 1 : from[1] == to[1] ? 0 : -1;
            while (from[0] != to[0] || from[1] != to[1]) {
               grid[from[0], from[1]] += 1;
               from[0] += stepx;
               from[1] += stepy;
            }
            grid[from[0], from[1]] += 1;
         }
         return grid.Cast<int>().Count(n => n > 1).ToString();
      }
   }
}
