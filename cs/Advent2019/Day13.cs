using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day13 : AdventDay {
      public override int Day => 13;
      public override int Year => 2019;

      public override string A() {
         IntcodeComputer brain = new IntcodeComputer(Input);
         int blocks = 0;
         int index = 1;
         brain.OnOutput += val => {
            if (index++ % 3 == 0 && val == 2)
               blocks++;
         };
         brain.Start();
         return blocks.ToString();
      }

      public override string B() {
         long score = 0;
         long[] program = Input.Split(",").Select(long.Parse).ToArray();
         program[0] = 2;
         IntcodeComputer brain = new IntcodeComputer(program);

         long ball = 0;
         long paddle = 0;
         int index = 0;
         long lastX = 0;
         brain.OnOutput += val => {
            index++;
            if (index == 1)
               lastX = val;
            else if (index == 3) {
               index = 0;
               if (lastX == -1)
                  score = val;
               else if (val == 3)
                  paddle = lastX;
               else if (val == 4)
                  ball = lastX;
            }
         };

         brain.WantsInput += () => paddle < ball ? 1 : paddle > ball ? -1 : 0;

         brain.Start();
         return score.ToString();
      }
   }
}
