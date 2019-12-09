using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class IntcodeComputer {
      public IntcodeComputer(string program, params long[] inputs) {
         Program = program.Split(",").Select(long.Parse).ToList();
         foreach (long input in inputs)
            Inputs.Enqueue(input);
      }

      public IntcodeComputer(long[] program, params long[] inputs) {
         Program = program.ToList();
         foreach (long input in inputs)
            Inputs.Enqueue(input);
      }

      private readonly Queue<long> Inputs = new Queue<long>();
      private int Pointer = 0;
      private readonly List<long> Program;
      private int RelativeBase = 0;

      /// <summary>The last value output by the computer</summary>
      public long Output { get; private set; } = -1;

      public delegate void HaltHandler();
      public delegate void OutputHandler(long output);
      public delegate void YieldHandler();

      public event HaltHandler OnHalt;
      public event OutputHandler OnOutput;
      public event YieldHandler OnYield;

      private int GetMode(int offset) {
         int div = 10;
         for (int i = 0; i < offset; i++)
            div *= 10;
         return (int) GetValue(Pointer) / div % 10;
      }

      private long GetParam(int offset) {
         long arg = GetValue(Pointer + offset);
         switch (GetMode(offset)) {
            case 0: // Position mode
               return GetValue((int) arg);
            case 1: // Parameter mode
               return arg;
            case 2: // Relative mode
               return GetValue(RelativeBase + (int) arg);
            default:
               throw new InvalidOperationException(
                  $"Invalid opcode: {Program[Pointer]}");
         }
      }

      private long GetValue(int index) {
         if (index > Program.Count)
            Program.AddRange(Enumerable.Repeat(0L, index - Program.Count + 2));
         return Program[index];
      }

      private void SetResult(int offset, long value) {
         int index = (int) GetValue(Pointer + offset);
         if (GetMode(offset) == 2)
            index += RelativeBase;
         if (index > Program.Count)
            Program.AddRange(Enumerable.Repeat(0L, index - Program.Count + 2));
         Program[index] = value;
         Pointer += offset + 1;
      }

      private void Run() {
         while (true) {
            switch (GetValue(Pointer) % 100) {
               case 1: // Addition
                  SetResult(3, GetParam(1) + GetParam(2));
                  break;
               case 2: // Multiplication
                  SetResult(3, GetParam(1) * GetParam(2));
                  break;
               case 3: // Input
                  if (!Inputs.Any()) {
                     OnYield?.Invoke();
                     return;
                  }
                  SetResult(1, Inputs.Dequeue());
                  break;
               case 4: // Output
                  Output = GetParam(1);
                  Pointer += 2;
                  OnOutput?.Invoke(Output);
                  break;
               case 5: // Jump-If-True
                  Pointer = GetParam(1) == 0 ? Pointer + 3 : (int) GetParam(2);
                  break;
               case 6: // Jump-If-False
                  Pointer = GetParam(1) != 0 ? Pointer + 3 : (int) GetParam(2);
                  break;
               case 7: // Less-Than
                  SetResult(3, GetParam(1) < GetParam(2) ? 1 : 0);
                  break;
               case 8: // Equals
                  SetResult(3, GetParam(1) == GetParam(2) ? 1 : 0);
                  break;
               case 9: // Adjust-Relative-Base
                  RelativeBase += (int) GetParam(1);
                  Pointer += 2;
                  break;
               case 99:
                  OnHalt?.Invoke();
                  return;
               default:
                  throw new InvalidOperationException(
                     $"Invalid instruction: {Program[Pointer]}");
            }
         }
      }

      /// <summary>Push an input onto the input queue</summary>
      public void Push(long input) {
         Inputs.Enqueue(input);
      }

      /// <summary>Start the event-driven computer</summary>
      public void Start() {
         Run();
      }

      /// <summary>
      /// Start the computer a single time, and return the last output.
      /// </summary>
      /// <param name="program">
      /// A comma-separated string of integer instructions
      /// </param>
      /// <param name="inputs">Program inputs</param>
      /// <returns>The last "output" value output by the computer</returns>
      public static long Run(string program, params long[] inputs) {
         IntcodeComputer computer = new IntcodeComputer(program, inputs);
         computer.Start();
         return computer.Output;
      }

      /// <summary>
      /// Start the computer a single time, and return the last output.
      /// </summary>
      /// <param name="program">
      /// An integer array instructions
      /// </param>
      /// <param name="inputs">Program inputs</param>
      /// <returns>The last "output" value output by the computer</returns>
      public static long Run(ref long[] program, params long[] inputs) {
         IntcodeComputer computer = new IntcodeComputer(program, inputs);
         computer.Start();
         program = computer.Program.ToArray();
         return computer.Output;
      }
   }
}
