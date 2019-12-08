using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class IntcodeComputer {
      public IntcodeComputer() { }

      public IntcodeComputer(string program, params int[] inputs)
         : this(program.Split(",").Select(int.Parse).ToArray(), inputs) { }

      public IntcodeComputer(int[] program, params int[] inputs) {
         Program = program;
         foreach (int input in inputs)
            Inputs.Enqueue(input);
      }

      private readonly Queue<int> Inputs = new Queue<int>();
      private int InsPointer = 0;
      private readonly int[] Program;

      public int Output { get; private set; } = -1;

      private int GetParam(int[] program, int index, int param) {
         int div = 10;
         for (int i = 0; i < param; i++)
            div *= 10;
         int arg = program[index + param];
         bool byVal = (program[index] / div % 10) == 1;
         return byVal ? arg : program[arg];
      }

      public delegate void HaltHandler();
      public delegate void OutputHandler(int output);
      public delegate void YieldHandler();

      public event HaltHandler OnHalt;
      public event OutputHandler OnOutput;
      public event YieldHandler OnYield;

      private void Run() {
         while (true) {
            switch(Program[InsPointer] % 100) {
               case 1: // Addition
                  Program[Program[InsPointer + 3]] =
                     GetParam(Program, InsPointer, 1) +
                     GetParam(Program, InsPointer, 2);
                  InsPointer += 4;
                  break;
               case 2: // Multiplication
                  Program[Program[InsPointer + 3]] =
                     GetParam(Program, InsPointer, 1) *
                     GetParam(Program, InsPointer, 2);
                  InsPointer += 4;
                  break;
               case 3: // Input
                  if (!Inputs.Any()) {
                     OnYield?.Invoke();
                     return;
                  }
                  Program[Program[InsPointer + 1]] = Inputs.Dequeue();
                  InsPointer += 2;
                  break;
               case 4: // Output
                  Output = GetParam(Program, InsPointer, 1);
                  InsPointer += 2;
                  OnOutput?.Invoke(Output);
                  break;
               case 5: // Jump-If-True
                  if (GetParam(Program, InsPointer, 1) == 0)
                     InsPointer += 3;
                  else
                     InsPointer = GetParam(Program, InsPointer, 2);
                  break;
               case 6: // Jump-If-False
                  if (GetParam(Program, InsPointer, 1) != 0)
                     InsPointer += 3;
                  else
                     InsPointer = GetParam(Program, InsPointer, 2);
                  break;
               case 7: // Less-Than
                  Program[Program[InsPointer + 3]] =
                     GetParam(Program, InsPointer, 1) <
                     GetParam(Program, InsPointer, 2) ? 1 : 0;
                  InsPointer += 4;
                  break;
               case 8: // Equals
                  Program[Program[InsPointer + 3]] =
                     GetParam(Program, InsPointer, 1) ==
                     GetParam(Program, InsPointer, 2) ? 1 : 0;
                  InsPointer += 4;
                  break;
               case 99:
                  OnHalt?.Invoke();
                  return;
               default:
                  throw new InvalidOperationException(
                     $"Invalid instruction: {Program[InsPointer]}");
            }
         }
      }

      public void Push(int input) {
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
      public static int Run(string program, params int[] inputs) {
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
      public static int Run(int[] program, params int[] inputs) {
         IntcodeComputer computer = new IntcodeComputer(program, inputs);
         computer.Start();
         return computer.Output;
      }
   }
}
