using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Advent2019 {
   public class Day12 : AdventDay {
      public Day12() {
         Moons = InputLines.Select(line => {
            Match re = Regex.Match(line, @"<x=(-?\d*), y=(-?\d*), z=(-?\d*)>");
            return new Moon(
               int.Parse(re.Groups[1].Value),
               int.Parse(re.Groups[2].Value),
               int.Parse(re.Groups[3].Value));
         }).ToArray();
      }

      public override int Day => 12;
      public override int Year => 2019;

      private readonly Moon[] Moons;

      private int Compare(int a, int b) {
         return a > b ? -1 : a < b ? 1 : 0;
      }

      private IEnumerable<T[]> Combinations<T>(IEnumerable<T> elements, int k) {
         return k == 0 ? new[] { new T[0] } :
            elements.SelectMany((element, index) =>
               Combinations(elements.Skip(index + 1), k - 1)
                  .Select(combos => combos.Prepend(element).ToArray()));
      }

      private long GCD(long a, long b) {
         a = Math.Abs(a);
         b = Math.Abs(b);
         while (b > 0) {
            long remainder = a % b;
            a = b;
            b = remainder;
         }
         return a;
      }

      private (int, int, int, int) GetPos(int c) {
         return (
            Moons[0].Pos[c],
            Moons[1].Pos[c],
            Moons[2].Pos[c],
            Moons[3].Pos[c]
         );
      }

      private (int, int, int, int) GetVel(int c) {
         return (
            Moons[0].Vel[c],
            Moons[1].Vel[c],
            Moons[2].Vel[c],
            Moons[3].Vel[c]
         );
      }

      private long LCD(long a, long b) {
         return a * b / GCD(a, b);
      }

      private long LCD(long a, long b, long c) {
         return LCD(a, LCD(b, c));
      }

      private void UpdatePosition(Moon moon) {
         for (int c = 0; c < 3; c++)
            moon.Pos[c] += moon.Vel[c];
      }

      private void UpdatePosition(Moon moon, int c) {
         moon.Pos[c] += moon.Vel[c];
      }

      private void UpdateVelocities(Moon[] moons) {
         for (int c = 0; c < 3; c++) {
            moons[0].Vel[c] += Compare(moons[0].Pos[c], moons[1].Pos[c]);
            moons[1].Vel[c] += Compare(moons[1].Pos[c], moons[0].Pos[c]);
         }
      }

      private void UpdateVelocities(Moon[] moons, int c) {
         moons[0].Vel[c] += Compare(moons[0].Pos[c], moons[1].Pos[c]);
         moons[1].Vel[c] += Compare(moons[1].Pos[c], moons[0].Pos[c]);
      }

      public override string A() {
         for (int x = 0; x < 1000; x++) {
            foreach (Moon[] pair in Combinations(Moons, 2))
               UpdateVelocities(pair);
            foreach (Moon moon in Moons)
               UpdatePosition(moon);
         }

         return Moons.Sum(moon => moon.TotalEnergy).ToString();
      }

      public override string B() {
         int count = 0;
         Moon[][] pairs = Combinations(Moons, 2).ToArray();
         (int, int, int, int)[] origins = Enumerable.Range(0, 3)
            .Select(c => GetPos(c))
            .ToArray();
         int[] periods = new[] { -1, -1, -1 };
         int done = 0;
         while (done < 3) {
            done = 0;
            count++;
            for (int c = 0; c < 3; c++) {
               if (periods[c] > 0) {
                  done++;
                  continue;
               }
               foreach (Moon[] pair in pairs)
                  UpdateVelocities(pair, c);
               foreach (Moon moon in Moons)
                  UpdatePosition(moon, c);
               if (GetPos(c) == origins[c] && GetVel(c) == (0, 0, 0, 0)) {
                  periods[c] = count;
                  done++;
               }
            }
            if (count % 1000000 == 0)
               Console.WriteLine(count);
         }

         return LCD(periods[0], periods[1], periods[2]).ToString();
      }

      private class Moon {
         public Moon() { }
         public Moon(int x, int y, int z) {
            Pos[0] = x;
            Pos[1] = y;
            Pos[2] = z;
         }

         public int[] Pos { get; } = new int[3];
         public int[] Vel { get; } = new int[3];

         public int KineticEnergy => Vel.Sum(v => Math.Abs(v));
         public int PotentialEnergy => Pos.Sum(p => Math.Abs(p));
         public int TotalEnergy => PotentialEnergy * KineticEnergy;
      }
   }
}
