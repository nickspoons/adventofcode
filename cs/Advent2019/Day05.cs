using System;
using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day05 : AdventDay {
      protected override int Day => 5;
      protected override int Year => 2019;

      private int GetParam(int[] program, int index, int param) {
         int div = 10;
         for (int i = 0; i < param; i++)
            div *= 10;
         int arg = program[index + param];
         bool byVal = (program[index] / div % 10) == 1;
         return byVal ? arg : program[arg];
      }

      private int Run(int input) {
         int[] program = Input.Split(",").Select(int.Parse).ToArray();
         int i = 0;
         int output = -1;
         while (program[i] != 99) {
            switch(program[i] % 100) {
               case 1: // Addition
                  program[program[i + 3]] = GetParam(program, i, 1) + GetParam(program, i, 2);
                  i += 4;
                  break;
               case 2: // Multiplication
                  program[program[i + 3]] = GetParam(program, i, 1) * GetParam(program, i, 2);
                  i += 4;
                  break;
               case 3: // Input
                  program[program[i + 1]] = input;
                  i += 2;
                  break;
               case 4: // Output
                  output = GetParam(program, i, 1);
                  i += 2;
                  break;
               case 5: // Jump-If-True
                  if (GetParam(program, i, 1) == 0)
                     i += 3;
                  else
                     i = GetParam(program, i, 2);
                  break;
               case 6: // Jump-If-False
                  if (GetParam(program, i, 1) != 0)
                     i += 3;
                  else
                     i = GetParam(program, i, 2);
                  break;
               case 7: // Less-Than
                  program[program[i + 3]] = GetParam(program, i, 1) < GetParam(program, i, 2) ? 1 : 0;
                  i += 4;
                  break;
               case 8: // Equals
                  program[program[i + 3]] = GetParam(program, i, 1) == GetParam(program, i, 2) ? 1 : 0;
                  i += 4;
                  break;
               default:
                  throw new InvalidOperationException(
                     $"Invalid instruction: {program[i]}");
            }
         }
         return output;
      }

      public override string A() {
         return Run(1).ToString();
      }

      public override string B() {
         return Run(5).ToString();
      }
   }
}
