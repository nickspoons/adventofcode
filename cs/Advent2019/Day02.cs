using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day02 : AdventDay {
      public override int Day => 2;
      public override int Year => 2019;

      private IntcodeComputer Computer => new IntcodeComputer();

      public override string A() {
         int[] program = Input.Split(",").Select(int.Parse).ToArray();
         program[1] = 12;
         program[2] = 2;
         Computer.Run(program).ToString();
         return program[0].ToString();
      }

      public override string B() {
         int[] originalProgram = Input.Split(",").Select(int.Parse).ToArray();
         int target = 19690720;
         int a = 12;
         int previous = 0;
         int result = 0;
         while (result < target) {
            previous = result;
            int[] program = (int[]) originalProgram.Clone();
            program[1] = ++a;
            program[2] = 0;
            Computer.Run(program);
            result = program[0];
         }
         int b = target - previous;
         return (((a - 1) * 100) + b).ToString();
      }
   }
}
