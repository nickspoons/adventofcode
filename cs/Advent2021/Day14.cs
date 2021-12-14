using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Advent2021 {
   public class Day14 : AdventDay {
      public override int Day => 14;
      public override int Year => 2021;

      private Dictionary<string, char> Rules;
      private string Template;

      private void ParseInput() {
         Rules = new Dictionary<string, char>();
         Template = InputLines[0];
         foreach (string line in InputLines.Skip(1)) {
            string[] parts = line.Split(" -> ");
            Rules.Add(parts[0], parts[1][0]);
         }
      }

      private void ApplyRules(int times) {
         int n = 0;
         while (n++ < times) {
            string next = Template.Substring(0, 1);
            for (int i = 0; i < Template.Length - 1; i++) {
               char c = Rules[Template.Substring(i, 2)];
               next = $"{next}{c}{Template[i + 1]}";
            }
            Template = next;
         }
      }

      private int CommonalityDifference() {
         IEnumerable<IGrouping<char, char>> grouped = Template.ToCharArray().GroupBy(c => c);
         int most = grouped.Max(g => g.Count());
         int fewest = grouped.Min(g => g.Count());
         return most - fewest;
      }

      public override string A() {
         ParseInput();
         ApplyRules(10);
         return CommonalityDifference().ToString();
      }

      public override string B() {
         ParseInput();
         // ApplyRules(40);
         // 17 takes 20 seconds ... 40 is out of the question
         ApplyRules(17);
         return CommonalityDifference().ToString();
      }
   }
}
