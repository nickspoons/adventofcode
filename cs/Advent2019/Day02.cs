using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day02 : AdventDay {
      protected override int Day => 2;
      protected override int Year => 2019;

      private int Run(int a, int b) {
         int[] program = Input.Split(",").Select(int.Parse).ToArray();
         program[1] = a;
         program[2] = b;
         int i = 0;
         while (program[i] != 99) {
            program[program[i + 3]] = program[i] == 1
               ? program[program[i + 1]] + program[program[i + 2]]
               : program[program[i + 1]] * program[program[i + 2]];
            i += 4;
         }
         return program[0];
      }

      public override string A() {
         return Run(12, 2).ToString();
      }

      public override string B() {
         int target = 19690720;
         int a = 12;
         int previous = 0;
         int result = 0;
         while (result < target) {
            previous = result;
            result = Run(++a, 0);
         }
         int b = target - previous;
         return (((a - 1) * 100) + b).ToString();
      }
   }
}
