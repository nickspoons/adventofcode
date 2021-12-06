using System.Linq;

namespace AdventOfCode.Advent2021 {
   public class Day06 : AdventDay {
      public override int Day => 6;
      public override int Year => 2021;

      private long Extrapolate(int period) {
         long[] state = new long[7];
         foreach (int f in Input.Split(',').Select(s => int.Parse(s)))
            state[f] += 1;
         int index = 0;
         long plus2 = 0;
         long plus1 = 0;
         for (int days = 0; days < period; days++) {
            long plus0 = plus1;
            plus1 = plus2;
            plus2 = state[index];
            state[index] += plus0;
            index = ++index % 7;
         }
         return state.Sum() + plus1 + plus2;
      }

      public override string A() {
         return Extrapolate(80).ToString();
      }

      public override string B() {
         return Extrapolate(256).ToString();
      }
   }
}
