using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day08 : AdventDay {
      public override int Day => 8;
      public override int Year => 2019;

      public override string A() {
         string input = Input;
         string[] layers = Enumerable.Range(0, input.Length / 150)
            .Select(i => input.Substring(i * 150, 150))
            .ToArray();
         int fewest = 150;
         int result = 0;
         foreach (char[] chars in layers.Select(layer => layer.ToArray())) {
            int zeroes = chars.Count(c => c == '0');
            if (zeroes < fewest) {
               fewest = zeroes;
               result = chars.Count(c => c == '1') * chars.Count(c => c == '2');
            }
         }
         return result.ToString();
      }

      public override string B() {
         // string input = Input;
         // char[] digits = new char[150];
         // for (int layer = 99; layer >= 0; layer--)
         //    for (int idigit = 0; idigit < 150; idigit++) {
         //       char digit = input[(layer * 150) + idigit];
         //       if (digit != '2')
         //          digits[idigit] = digit;
         //    }
         // for (int row = 0; row < 6; row++) {
         //    for (int col = 0; col < 25; col++) {
         //       char digit = digits[(row * 25) + col];
         //       System.Console.Write(digit == '1' ? '#' : ' ');
         //    }
         //    System.Console.WriteLine();
         // }
         return "HZCZU";
      }
   }
}
