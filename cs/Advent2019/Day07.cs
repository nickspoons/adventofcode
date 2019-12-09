using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day07 : AdventDay {
      public override int Day => 7;
      public override int Year => 2019;

      private IEnumerable<int[]> GetPermutations(int[] items, int i = 0) {
         int n = items.Length;

         if (i >= n - 1)
            yield return items;
         else {
            foreach (int[] perm in GetPermutations(items, i + 1))
               yield return perm;
            for (int j = i + 1; j < n; j++) {
               int[] permItems = (int[]) items.Clone();
               permItems[i] = items[j];
               permItems[j] = items[i];
               foreach (int[] perm in GetPermutations(permItems, i + 1))
                  yield return perm;
            }
         }
      }

      public override string A() {
         long biggest = 0;
         foreach (int[] phases in GetPermutations(new[] { 0, 1, 2, 3, 4 })) {
            long input = 0;
            foreach (int phase in phases)
               input = IntcodeComputer.Run(Input, phase, input);
            if (input > biggest)
               biggest = input;
         }
         return biggest.ToString();
      }

      public override string B() {
         long biggest = 0;
         foreach (int[] phases in GetPermutations(new[] { 5, 6, 7, 8, 9 })) {
            IntcodeComputer[] amps = phases
               .Select(phase => new IntcodeComputer(Input, phase))
               .ToArray();
            for (int i = 0; i < 5; i++) {
               int next = i == 4 ? 0 : i + 1;
               amps[i].OnOutput += output => amps[next].Push(output);
               amps[i].OnYield += () => amps[next].Start();
               if (i < 4)
                  amps[i].OnHalt += () => amps[next].Start();
            }
            amps[0].Push(0);
            amps[0].Start();
            if (amps[4].Output > biggest)
               biggest = amps[4].Output;
         }
         return biggest.ToString();
      }
   }
}
