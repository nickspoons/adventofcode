using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Advent2021 {
   public class Day09 : AdventDay {
      public override int Day => 9;
      public override int Year => 2021;

      private int[][] _depths;
      private int[][] Depths => _depths ?? (_depths =
         InputLines
            .Select(l => l.Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray());

      private IEnumerable<Coord> FindBasins() {
         int maxy = Depths.Length;
         int maxx = Depths[0].Length;
         for (int y = 0; y < maxy; y++)
            for (int x = 0; x < maxx; x++) {
#pragma warning disable IDE0055
               bool above = y == 0        || Depths[y - 1][x] > Depths[y][x];
               bool below = y == maxy - 1 || Depths[y + 1][x] > Depths[y][x];
               bool left =  x == 0        || Depths[y][x - 1] > Depths[y][x];
               bool right = x == maxx - 1 || Depths[y][x + 1] > Depths[y][x];
#pragma warning restore IDE0055
               if (above && below && left && right)
                  yield return new(x, y);
            }
      }

      public override string A() {
         return FindBasins().Sum(b => Depths[b.y][b.x] + 1).ToString();
      }

      private void SearchBasin(HashSet<Coord> seen, Coord c) {
         int maxy = Depths.Length;
         int maxx = Depths[0].Length;
         if (c.x < 0 || c.y < 0 || c.x == maxx || c.y == maxy)
            return;
         if (Depths[c.y][c.x] == 9 || seen.Contains(c))
            return;
         seen.Add(c);
         SearchBasin(seen, new(c.x, c.y - 1));
         SearchBasin(seen, new(c.x, c.y + 1));
         SearchBasin(seen, new(c.x - 1, c.y));
         SearchBasin(seen, new(c.x + 1, c.y));
      }

      public override string B() {
         List<int> basinSizes = new List<int>();
         foreach (Coord basin in FindBasins()) {
            HashSet<Coord> seen = new HashSet<Coord>();
            SearchBasin(seen, basin);
            basinSizes.Add(seen.Count);
         }
         return basinSizes
            .OrderByDescending(n => n)
            .Take(3)
            .Aggregate(1, (acc, val) => acc * val)
            .ToString();
      }

      private record Coord(int x, int y);
   }
}
