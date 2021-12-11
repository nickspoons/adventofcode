using System.Linq;

namespace AdventOfCode.Advent2021 {
   public class Day11 : AdventDay {
      public override int Day => 11;
      public override int Year => 2021;

      private int[][] _octopodes;
      private int[][] Octopodes => _octopodes ?? (_octopodes =
         InputLines
            .Select(line => line.ToCharArray().Select(c => c - '0').ToArray())
            .ToArray());

      private bool[][] Flashed { get; set; }

      private int Flashes { get; set; }

      private void Increment(int x, int y) {
         Octopodes[y][x] += 1;
         if (Octopodes[y][x] > 9 && !Flashed[y][x]) {
            Flashed[y][x] = true;
            Flashes += 1;
            int h = Octopodes.Length;
            int w = Octopodes[0].Length;
            for (int yn = y > 0 ? y - 1 : y; yn <= (y < h - 1 ? y + 1 : y); yn++)
               for (int xn = x > 0 ? x - 1 : x; xn <= (x < w - 1 ? x + 1 : x); xn++)
                  Increment(xn, yn);
         }
      }

      private void ResetFlashes() {
         if (Flashed == null) {
            Flashed = new bool[Octopodes.Length][];
            for (int x = 0; x < Octopodes.Length; x++)
               Flashed[x] = new bool[Octopodes[0].Length];
         }
         else
            for (int y = 0; y < Octopodes.Length; y++)
               for (int x = 0; x < Octopodes[0].Length; x++)
                  Flashed[y][x] = false;
      }

      public override string A() {
         for (int n = 0; n < 100; n++) {
            ResetFlashes();
            for (int y = 0; y < Octopodes.Length; y++)
               for (int x = 0; x < Octopodes[0].Length; x++)
                  Increment(x, y);
            // Reset Octopodes > 9 to 0
            for (int y = 0; y < Octopodes.Length; y++)
               for (int x = 0; x < Octopodes[0].Length; x++)
                  if (Octopodes[y][x] > 9)
                     Octopodes[y][x] = 0;
         }
         return Flashes.ToString();
      }

      public override string B() {
         int n = 0;
         while (true) {
            ResetFlashes();
            for (int y = 0; y < Octopodes.Length; y++)
               for (int x = 0; x < Octopodes[0].Length; x++)
                  Increment(x, y);
            // Reset Octopodes > 9 to 0
            for (int y = 0; y < Octopodes.Length; y++)
               for (int x = 0; x < Octopodes[0].Length; x++)
                  if (Octopodes[y][x] > 9)
                     Octopodes[y][x] = 0;
            n++;
            if (Octopodes.Sum(r => r.Sum()) == 0)
               return n.ToString();
         }
      }
   }
}
