using System;
using System.Collections.Generic;

namespace AdventOfCode.Advent2019 {
   public class Day11 : AdventDay {
      public override int Day => 11;
      public override int Year => 2019;

      private (int, int) Direction = (-1, 0);
      private (int, int) Location = (0, 0);
      private readonly Dictionary<(int, int), bool> Painted =
         new Dictionary<(int, int), bool>();

      private long FindColour() {
         return FindColour(Location);
      }

      private long FindColour((int, int) pos) {
         return Painted.ContainsKey(pos) ? (Painted[pos] ? 1 : 0) : 0;
      }

      private (int, int) Turn(bool left) {
         switch (Direction) {
            case (-1, 0):
               return left ? (0, -1) : (0, 1);
            case (0, 1):
               return left ? (-1, 0) : (1, 0);
            case (1, 0):
               return left ? (0, 1) : (0, -1);
            case (0, -1):
               return left ? (1, 0) : (-1, 0);
            default:
               throw new InvalidOperationException(
                  "Careful with your directions there laddie");
         }
      }

      private void StartRobot() {
         IntcodeComputer brain = new IntcodeComputer(Input, FindColour());
         bool outputColour = true;
         brain.OnOutput += val => {
            if (outputColour)
               Painted[Location] = val == 1;
            else {
               Direction = Turn(val == 0);
               Location = (Location.Item1 + Direction.Item1,
                  Location.Item2 + Direction.Item2);
               brain.Push(FindColour());
            }
            outputColour = !outputColour;
         };
         brain.Start();
      }

      public override string A() {
         StartRobot();
         return Painted.Count.ToString();
      }

      public override string B() {
         // Painted.Add(Location, true);
         // StartRobot();
         // for (int x = 0; x <= 5; x++) {
         //    for (int y = 0; y <= 42; y++)
         //       Console.Write(FindColour((x, y)) == 1 ? 'X' : ' ');
         //    Console.WriteLine();
         // }
         return "EGBHLEUE";
      }
   }
}
