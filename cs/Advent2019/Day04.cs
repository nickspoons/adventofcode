namespace AdventOfCode.Advent2019 {
   public class Day04 : AdventDay {
      public override int Day => 4;
      public override int Year => 2019;

      // private const int FROM = 266666; // 264360
      // private const int TO = 699999; // 746325

      public override string A() {
         // Solved "by hand", in Vim
         return "945";
      }

      public override string B() {
         // The command used to exclude all invalid passwords was:
         // :v/\%(\(^\|[^3]\)33\%($\|[^3]\)\|\(^\|[^4]\)44\%($\|[^4]\)\|\(^\|[^5]\)55\%($\|[^5]\)\|\(^\|[^6]\)66\%($\|[^6]\)\|\(^\|[^7]\)77\%($\|[^7]\)\|\(^\|[^8]\)88\%($\|[^8]\)\|\(^\|[^9]\)99\%($\|[^9]\)\)/d
         return "617";
      }
   }
}
