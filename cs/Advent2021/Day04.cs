using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Advent2021 {
   public class Day04 : AdventDay {
      public override int Day => 4;
      public override int Year => 2021;

      private int[] InputDraws => InputLines[0].Split(",")
         .Select(s => int.Parse(s))
         .ToArray();

      // Shadow base InputLines, which removes empty lines
      private new string[] InputLines => Input.Trim().Split('\n');

      private List<List<(int, bool)[]>> ReadBoards() {
         List<List<(int, bool)[]>> boards = new List<List<(int, bool)[]>>();
         foreach (string line in InputLines.Skip(1)) {
            if (string.IsNullOrWhiteSpace(line))
               boards.Add(new List<(int, bool)[]>());
            else
               boards.Last().Add(
                  line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                     .Select(s => (int.Parse(s), false))
                     .ToArray());
         }
         return boards;
      }

      private static bool DrawBoard(int draw, List<(int, bool)[]> board) {
         // Draw
         for (int l = 0; l < board.Count; l++)
            for (int c = 0; c < board[l].Length; c++)
               if (board[l][c].Item1 == draw)
                  board[l][c].Item2 = true;
         // Check rows
         for (int l = 0; l < board.Count; l++)
            if (board[l].All(col => col.Item2))
               return true;
         // Check columns
         for (int c = 0; c < board.First().Length; c++)
            if (board.All(line => line[c].Item2))
               return true;
         return false;
      }

      public override string A() {
         List<List<(int, bool)[]>> boards = ReadBoards();
         foreach (int draw in InputDraws)
            foreach (List<(int, bool)[]> board in boards)
               if (DrawBoard(draw, board)) {
                  int sumUnmarked = board
                     .Sum(line => line
                        .Where(col => !col.Item2)
                        .Sum(col => col.Item1));
                  return (draw * sumUnmarked).ToString();
               }
         return "";
      }

      public override string B() {
         List<List<(int, bool)[]>> boards = ReadBoards();
         bool[] won = boards.Select(b => false).ToArray();
         foreach (int draw in InputDraws)
            for (int i = 0; i < boards.Count; i++) {
               List<(int, bool)[]> board = boards[i];
               if (DrawBoard(draw, board) && !won[i]) {
                  won[i] = true;
                  if (won.All(b => b)) {
                     int sumUnmarked = board
                        .Sum(line => line
                           .Where(col => !col.Item2)
                           .Sum(col => col.Item1));
                     return (draw * sumUnmarked).ToString();
                  }
               }
            }

         return "";
      }
   }
}
