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

      private void UpdatePosition(Moon moon) {
         moon.X += moon.VX;
         moon.Y += moon.VY;
         moon.Z += moon.VZ;
      }

      private void UpdateVelocities(Moon[] moons) {
         moons[0].VX += Compare(moons[0].X, moons[1].X);
         moons[1].VX += Compare(moons[1].X, moons[0].X);
         moons[0].VY += Compare(moons[0].Y, moons[1].Y);
         moons[1].VY += Compare(moons[1].Y, moons[0].Y);
         moons[0].VZ += Compare(moons[0].Z, moons[1].Z);
         moons[1].VZ += Compare(moons[1].Z, moons[0].Z);
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
         return "";
      }

      private class Moon {
         public Moon() { }
         public Moon(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;
         }

         public int X { get; set; }
         public int Y { get; set; }
         public int Z { get; set; }

         public int VX { get; set; } = 0;
         public int VY { get; set; } = 0;
         public int VZ { get; set; } = 0;

         public int KineticEnergy => Math.Abs(VX) + Math.Abs(VY) + Math.Abs(VZ);
         public int PotentialEnergy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
         public int TotalEnergy => PotentialEnergy * KineticEnergy;
      }
   }
}
