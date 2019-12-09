using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day02 : AdventDay {
      public override int Day => 2;
      public override int Year => 2019;

      public override string A() {
         long[] program = Input.Split(",").Select(long.Parse).ToArray();
         program[1] = 12;
         program[2] = 2;
         IntcodeComputer.Run(ref program);
         return program[0].ToString();
      }

      public override string B() {
         long[] originalProgram = Input.Split(",").Select(long.Parse).ToArray();
         long target = 19690720;
         long a = 12;
         long previous = 0;
         long result = 0;
         while (result < target) {
            previous = result;
            long[] program = (long[]) originalProgram.Clone();
            program[1] = ++a;
            program[2] = 0;
            IntcodeComputer.Run(ref program);
            result = program[0];
         }
         long b = target - previous;
         return (((a - 1) * 100) + b).ToString();
      }
   }
}
