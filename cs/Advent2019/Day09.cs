namespace AdventOfCode.Advent2019 {
   public class Day09 : AdventDay {
      public override int Day => 9;
      public override int Year => 2019;

      public override string A() {
         return IntcodeComputer.Run(Input, 1).ToString();
      }

      public override string B() {
         return IntcodeComputer.Run(Input, 2).ToString();
      }
   }
}
