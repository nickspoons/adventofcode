using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day10 : AdventDay {
      public Day10() {
         Space = InputLines.Select(line => line.ToArray()).ToArray();
         Height = Space.Length;
         Width = Space[0].Length;
         Depth = Math.Max(Height, Width) - 1;
      }

      public override int Day => 10;
      public override int Year => 2019;

      private readonly int Depth;
      private readonly int Height;
      private readonly char[][] Space;
      private readonly int Width;

      private IEnumerable<(int, int)> FindAsteroids() {
         for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
               if (Space[y][x] == '#')
                  yield return (x, y);
      }

      private bool InBounds(int x, int y) {
         return x >= 0 && x < Width
            && y >= 0 && y < Height;
      }

      private int Check(char[][] space, int ox, int oy, int x, int y) {
         if (!InBounds(x, y))
            return 0;
         char val = Read(space, x, y);
         if (val != '.' && val != '#') // Seen
            return 0;
         if (val != '#') {
            Write(space, x, y, '-');
            return 0;
         }
         Write(space, x, y, '0');

         int dx = x - ox;
         int dy = y - oy;
         if (dx == 0)
            dy = dy > 0 ? 1 : -1;
         else if (dy == 0)
            dx = dx > 0 ? 1 : -1;
         else {
            int gcd = GCD(dx, dy);
            dx /= gcd;
            dy /= gcd;
         }
         while (true) {
            x += dx;
            y += dy;
            if (!InBounds(x, y)) break;
            Write(space, x, y, 'X');
         };
         return 1;
      }

      private int Check(char[][] space, int x, int y) {
         int seen = 0;
         for (int depth = 1; depth <= Depth; depth++) {
            for (int n = -depth; n <= depth; n++) {
               seen += Check(space, x, y, x + n, y - depth);
               seen += Check(space, x, y, x + n, y + depth);
               seen += Check(space, x, y, x - depth, y + n);
               seen += Check(space, x, y, x + depth, y + n);
            }
         }
         return seen;
      }

      private void ClearChecked(char[][] space) {
         for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++) {
               char c = Read(space, x, y);
               Write(space, x, y, c == '0' ? '-' : c == 'X' ? '#' : c);
            }
      }

      private int GCD(int a, int b) {
         a = Math.Abs(a);
         b = Math.Abs(b);
         while (b > 0) {
            int remainder = a % b;
            a = b;
            b = remainder;
         }
         return a;
      }

      private char Read(char[][] space, int x, int y) {
         return space[y][x];
      }

      private void Write(char[][] space, int x, int y, char value) {
         space[y][x] = value;
      }

      public override string A() {
         int biggest = 0;
         foreach ((int, int) asteroid in FindAsteroids()) {
            char[][] space = Space.Select(line => line.ToArray()).ToArray();
            int seen = Check(space, asteroid.Item1, asteroid.Item2);
            if (seen > biggest)
               biggest = seen;
         }

         return biggest.ToString();
      }

      public override string B() {
         char[][] space = Space.Select(line => line.ToArray()).ToArray();
         int ox = 25;
         int oy = 31;
         int target = 200;
         int seen = 0;
         while (true) {
            int chkd = Check(space, ox, oy);
            if (chkd + seen >= target) break;
            ClearChecked(space);
            seen += chkd;
         }
         List<(double, int, int)> angles = new List<(double, int, int)>();
         for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
               if (Read(space, x, y) == '0') {
                  double angle = Math.Atan2(y - oy, x - ox) * (180 / Math.PI);
                  // Adjust from -180:180 to 0:360, with 0 pointing up
                  angle += 90;
                  if (angle < 0)
                     angle += 360;
                  angles.Add((angle, x, y));
               }
            }
         }
         (double, int, int) nth = angles
            .OrderBy(angle => angle.Item1)
            .ElementAt(target - seen - 1);
         return ((nth.Item2 * 100) + nth.Item3).ToString();
      }
   }
}
