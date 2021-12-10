using System.Collections.Generic;

namespace AdventOfCode.Advent2021 {
   public class Day10 : AdventDay {
      public override int Day => 10;
      public override int Year => 2021;

      private readonly Dictionary<char, char> Pairs = new Dictionary<char, char> {
         { '(', ')' },
         { '[', ']' },
         { '{', '}' },
         { '<', '>' }
      };

      public override string A() {
         int score = 0;
         Dictionary<char, int> Scores = new Dictionary<char, int> {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
         };
         foreach (string line in InputLines) {
            Stack<char> queue = new Stack<char>();
            foreach (char c in line.ToCharArray()) {
               if (Pairs.ContainsKey(c))
                  queue.Push(Pairs[c]);
               else if (queue.Pop() != c) {
                  score += Scores[c];
                  break;
               }
            }
         }
         return score.ToString();
      }

      public override string B() {
         List<long> scores = new List<long>();
         Dictionary<char, int> Scores = new Dictionary<char, int> {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 }
         };
         foreach (string line in InputLines) {
            Stack<char> queue = new Stack<char>();
            bool invalid = false;
            foreach (char c in line.ToCharArray()) {
               if (Pairs.ContainsKey(c))
                  queue.Push(Pairs[c]);
               else if (queue.Pop() != c) {
                  invalid = true;
                  break;
               }
            }
            if (invalid)
               continue;
            long score = 0;
            foreach (char c in queue)
               score = (score * 5) + Scores[c];
            scores.Add(score);
         }
         scores.Sort();
         long middle = scores[scores.Count / 2];
         return middle.ToString();
      }
   }
}
