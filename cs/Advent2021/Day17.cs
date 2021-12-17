using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Advent2021 {
   public class Day17 : AdventDay {
      public override int Day => 17;
      public override int Year => 2021;

      private int VXMax { get; set; }
      private int VXMin { get; set; }
      private int VYMax { get; set; }
      private int[] Target { get; set; }

      private void FindArchingLimits() {
         int vx = 0;
         int d = 0;
         while (d < Target[0])
            d += ++vx;
         VXMin = vx;
         while (d <= Target[1])
            d += ++vx;
         VXMax = vx;
         VYMax = 0;
         bool yAtZero = false;
         while (true) {
            VYMax++;
            int y = 0, vy = VYMax;
            while (y >= Target[2]) {
               if (yAtZero)
                  yAtZero = false;
               if (y == 0)
                  yAtZero = true;
               y += vy--;
            }
            if (yAtZero)
               break;
         }
      }

      private void ParseInput() {
         Match re = Regex.Match(Input.Trim(), @"x=(-?\d+)\.\.(-?\d+), y=(-?\d+)\.\.(-?\d+)");
         Target = new[] {
            int.Parse(re.Groups[1].Value),
            int.Parse(re.Groups[2].Value),
            int.Parse(re.Groups[3].Value),
            int.Parse(re.Groups[4].Value)
         };
      }

      private int TestArching(int vx, int vy) {
         int x = 0, y = 0;
         int highest = 0;
         while (x <= Target[1] && y >= Target[2]) {
            if (x >= Target[0] && y <= Target[3])
               return highest;
            if (y > highest)
               highest = y;
            if (vx > 0)
               x += vx--;
            y += vy--;
         }
         return -1;
      }

      private bool TestFlat(int vx, int vy) {
         int x = 0, y = 0;
         while (x <= Target[1] && y >= Target[2]) {
            if (x >= Target[0] && y <= Target[3])
               return true;
            if (vx > 0)
               x += vx--;
            y += vy--;
         }
         return false;
      }

      public override string A() {
         ParseInput();
         FindArchingLimits();
         Console.WriteLine(VYMax);
         int highest = 0;
         for (int vx = VXMin; vx <= VXMax; vx++)
            for (int vy = 0; vy <= VYMax; vy++) {
               int height = TestArching(vx, vy);
               if (height > highest)
                  highest = height;
            }
         return highest.ToString();
      }

      public override string B() {
         ParseInput();
         FindArchingLimits();
         int count = 0;
         // Find the arcing trajectories that go up, then down
         for (int vx = VXMin; vx <= VXMax * 2; vx++)
            for (int vy = 0; vy <= VYMax; vy++)
               if (TestArching(vx, vy) >= 0)
                  count++;
         // Find the flatter trajectores
         for (int vx = VXMin; vx <= Target[1]; vx++)
            for (int vy = -1; vy >= Target[2]; vy--)
               if (TestFlat(vx, vy))
                  count++;
         return count.ToString();
      }
   }
}
