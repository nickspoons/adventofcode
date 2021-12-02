namespace AdventOfCode.Advent2021 {
   public class Day01 : AdventDay {
      public override int Day => 1;
      public override int Year => 2021;

      public override string A() {
         int[] depths = InputIntLines;
         int previous = depths[0];
         int increases = 0;
         for (int i = 1; i < depths.Length; i++) {
            if (depths[i] > previous)
               increases++;
            previous = depths[i];
         }
         return increases.ToString();
      }

      public override string B() {
         int[] depths = InputIntLines;
         int previous = depths[0] + depths[1] + depths[2];
         int increases = 0;
         for (int i = 3; i < depths.Length; i++) {
            int next = previous + depths[i] - depths[i - 3];
            if (next > previous)
               increases++;
            previous = next;
         }
         return increases.ToString();
      }
   }
}
