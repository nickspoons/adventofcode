using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Advent2021 {
   public class Day08 : AdventDay {
      public override int Day => 8;
      public override int Year => 2021;

      public override string A() {
         int n = 0;
         int[] lengths = { 2, 3, 4, 7 };
         foreach (string line in InputLines) {
            string after = line.Split("|", StringSplitOptions.TrimEntries)[1];
            n += after.Split(' ').Count(s => lengths.Contains(s.Length));
         }
         return n.ToString();
      }

      public override string B() {
         int result = 0;
         static string sort(IEnumerable<char> input) => string.Join("", input.OrderBy(c => c));
         foreach (string line in InputLines) {
            string[] parts = line.Split("|", StringSplitOptions.TrimEntries);
            string[] before = parts[0].Split(' ').Select(s => sort(s)).ToArray();
            string[] after = parts[1].Split(' ').Select(s => sort(s)).ToArray();
            string[] digits = before.Concat(after).ToArray();
            Dictionary<int, string> values = new Dictionary<int, string>();
            void append(int key, IEnumerable<char> digit) {
               if (!values.ContainsKey(key))
                  values.Add(key, sort(digit));
            }
            foreach (string digit in digits) {
               if (digit.Length == 2) append(1, digit);
               if (digit.Length == 3) append(7, digit);
               if (digit.Length == 4) append(4, digit);
               if (digit.Length == 7) append(8, digit);
            }
            char a = values[7].Except(values[1]).First();
            string partial = sort(values[4] + a);
            append(9, digits.First(d => d.Length == 6 && d.Except(partial).Count() == 1));
            char g = values[9].Except(partial).First();
            char e = values[8].Except(values[9]).First();
            IEnumerable<string> e35 = digits.Where(d => d.Length == 5 && values[9].Except(d).Count() == 1);
            append(3, e35.First(d => d.Intersect(values[1]).Count() == 2));
            append(5, e35.First(d => d.Intersect(values[1]).Count() == 1));
            char c = values[9].Except(values[5]).First();
            char f = values[1].Except(c.ToString()).First();
            append(6, values[5] + e);
            append(0, digits.First(d => d.Length == 6 && !values.Values.Contains(d)));
            char d = values[8].Except(values[0]).First();
            char b = values[4].Except(values[1]).Except(d.ToString()).First();
            append(2, values[8].Except(new[] { b, f }));
            int output = int.Parse(string.Join("", after.Select(digit =>
                     values.FirstOrDefault(x => x.Value == digit).Key)));
            result += output;
         }
         return result.ToString();
      }
   }
}
