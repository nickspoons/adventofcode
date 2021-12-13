using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CA1310

namespace AdventOfCode.Advent2021 {
   public class Day13 : AdventDay {
      public override int Day => 13;
      public override int Year => 2021;

      private List<Fold> Folds = new List<Fold>();
      private List<List<bool>> Points;
      private int MaxX { get; set; }
      private int MaxY { get; set; }

      private void ParseInput() {
         List<int[]> coords = new List<int[]>();
         foreach (string line in InputLines) {
            if (char.IsDigit(line[0]))
               coords.Add(line.Split(',').Select(s => int.Parse(s)).ToArray());
            else {
               string[] parts = line.Split('=');
               Folds.Add(new(parts[0][^1], int.Parse(parts[1])));
            }
         }
         MaxX = coords.Max(c => c[0]);
         MaxY = coords.Max(c => c[1]);
         Points = new List<List<bool>>();
         for (int y = 0; y <= MaxY; y++) {
            Points.Add(new List<bool>());
            for (int x = 0; x <= MaxX; x++)
               Points[y].Add(coords.Any(c => c[0] == x && c[1] == y));
         }
      }

      private void FoldPaper(Fold fold) {
         if (fold.axis == 'y') {
            for (int y = 1; y <= MaxY - fold.n; y++)
               for (int x = 0; x <= MaxX; x++)
                  if (Points[fold.n + y][x])
                     Points[fold.n - y][x] = true;
            MaxY = fold.n - 1;
         }
         else {
            for (int y = 0; y <= MaxY; y++)
               for (int x = 1; x <= MaxX - fold.n; x++)
                  if (Points[y][fold.n + x])
                     Points[y][fold.n - x] = true;
            MaxX = fold.n - 1;
         }
      }

      private void Paint() {
         for (int y = 0; y <= MaxY; y++) {
            for (int x = 0; x <= MaxX; x++)
               Console.Write(Points[y][x] ? 'X' : ' ');
            Console.WriteLine();
         }
      }

      public override string A() {
         ParseInput();
         FoldPaper(Folds[0]);
         return Points
            .Take(MaxY + 1)
            .Sum(p => p.Take(MaxX + 1).Count(b => b))
            .ToString();
      }

      public override string B() {
         ParseInput();
         foreach (Fold fold in Folds)
            FoldPaper(fold);
         // Paint();
         return "ECFHLHZF"; // Read from output
      }

      private record Fold(char axis, int n);
   }
}
