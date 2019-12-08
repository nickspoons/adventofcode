namespace AdventOfCode.Advent2019 {
   public class Day05 : AdventDay {
      public override int Day => 5;
      public override int Year => 2019;

      public override string A() {
         return IntcodeComputer.Run(Input, 1).ToString();
      }

      public override string B() {
         return IntcodeComputer.Run(Input, 5).ToString();
      }
   }
}
