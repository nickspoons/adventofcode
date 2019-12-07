using System;
using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day05 : AdventDay {
      public override int Day => 5;
      public override int Year => 2019;

      private IntcodeComputer Computer => new IntcodeComputer();

      public override string A() {
         return Computer.Run(Input, 1).ToString();
      }

      public override string B() {
         return Computer.Run(Input, 5).ToString();
      }
   }
}
