using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day01 : AdventDay {
      protected override int Day => 1;
      protected override int Year => 2019;

      private int GetFuel(int mass) {
         return (mass / 3) - 2;
      }

      private int GetAllFuel(int mass) {
         int fuel = GetFuel(mass);
         int diff = GetFuel(fuel);
         while (diff > 0) {
            fuel += diff;
            diff = GetFuel(diff);
         }
         return fuel;
      }

      public override string A() {
         return InputIntLines.Select(GetFuel).Sum().ToString();
      }

      public override string B() {
         return InputIntLines.Select(GetAllFuel).Sum().ToString();
      }
   }
}
