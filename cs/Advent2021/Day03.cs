using System;
using System.Linq;

namespace AdventOfCode.Advent2021 {
   public class Day03 : AdventDay {
      public override int Day => 3;
      public override int Year => 2021;

      public override string A() {
         string[] input = InputLines;
         int a = input.Length / 2;
         string gamma = "";
         string epsilon = "";
         for (int i = 0; i < input[0].Length; i++) {
            gamma += input.Count(il => il[i] == '1') >= a ? "1" : "0";
            epsilon += gamma[i] == '0' ? "1" : "0";
         }
         return (Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)).ToString();
      }

      public override string B() {
         string[] count(string[] inputs, int i, bool most) {
            int ones = inputs.Count(il => il[i] == '1');
            int zeroes = inputs.Count(il => il[i] == '0');
            bool choose1 = most ? ones >= zeroes : ones < zeroes;
            return inputs.Where(il => il[i] == (choose1 ? '1' : '0')).ToArray();
         }

         string[] input = InputLines;
         for (int i = 0; i < input[0].Length; i++) {
            input = count(input, i, true);
            if (input.Length == 1)
               break;
         }
         int generator = Convert.ToInt32(input[0], 2);

         input = InputLines;
         for (int i = 0; i < input[0].Length; i++) {
            input = count(input, i, false);
            if (input.Length == 1)
               break;
         }
         int scrubber = Convert.ToInt32(input[0], 2);

         return (generator * scrubber).ToString();
      }
   }
}
