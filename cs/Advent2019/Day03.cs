using System;
using System.Collections.Generic;

namespace AdventOfCode.Advent2019 {
   public class Day03 : AdventDay {
      // Bounds = {top: 3387, right: 7910, bottom: -12385, left: -5977}
      public override int Day => 3;
      public override int Year => 2019;

      private delegate void OnMove((int, int) point, int x, int y, int length);

      private void Move(string inputLine, OnMove onMove) {
         int x = 0; int y = 0; int length = 0;
         string[] moves = inputLine.Split(',');
         foreach (string move in moves) {
            char dir = move[0];
            int dist = int.Parse(move.Substring(1));
            for (int i = 0; i < dist; i++) {
               x += dir == 'U' ? 1 : dir == 'D' ? -1 : 0;
               y += dir == 'R' ? 1 : dir == 'L' ? -1 : 0;
               (int, int) point = (x, y);
               onMove(point, x, y, ++length);
            }
         }
      }

      private int Untangle(bool manhattan) {
         Dictionary<(int, int), int> points = new Dictionary<(int, int), int>();
         Move(InputLines[0], ((int, int) point, int x, int y, int length) => {
            if (!points.ContainsKey(point))
               points.Add(point, length);
         });
         int min = 100000;
         Move(InputLines[1], ((int, int) point, int x, int y, int length) => {
            if (points.ContainsKey(point)) {
               int distance = manhattan
                  ? Math.Abs(x) + Math.Abs(y)
                  : length + points[point];
               if (distance < min)
                  min = distance;
            }
         });
         return min;
      }

      public override string A() {
         return Untangle(true).ToString();
      }

      public override string B() {
         return Untangle(false).ToString();
      }
   }
}
