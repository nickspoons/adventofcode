using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Advent2019 {
   public class Day15 : AdventDay {
      public Day15() {
         Display = Enumerable.Range(0, HEIGHT)
            .Select(i => Enumerable.Range(0, WIDTH)
               .Select(_ => ' ')
               .ToArray())
            .ToArray();
      }

      public override int Day => 15;
      public override int Year => 2019;

      private const int WIDTH = 50;
      private const int HEIGHT = 50;

      private readonly bool Playing = false;
      private readonly bool Displaying = false;

      private readonly char[][] Display;
      private int X = WIDTH / 2;
      private int Y = HEIGHT / 2;
      private char Dir = 'k';
      private long NextInput = -1;

      private readonly Dictionary<char, int> DirToIns =
         new Dictionary<char, int> {
            { 'k', 1 },
            { 'j', 2 },
            { 'h', 3 },
            { 'l', 4 }
         };
      private readonly Dictionary<char, char> InvertDir =
         new Dictionary<char, char> {
            { 'k', 'j' },
            { 'j', 'k' },
            { 'h', 'l' },
            { 'l', 'h' }
         };

      private int Distance = 0;
      private readonly List<Location> Seen = new List<Location>();

      private TaskCompletionSource<long> IOCompletionSource;

      private Task<long> IO(long input) {
         NextInput = input;
         IOCompletionSource = new TaskCompletionSource<long>();
         return IOCompletionSource.Task;
      }

      private async Task<int> Explore() {
         Seen.Add(new Location(X, Y, Distance));
         (char, Location)[] options = await Look();
         char dir = 'h';
         Stack<(int, int, char)> forks = new Stack<(int, int, char)>();
         while (true) {
            if (options.Any(o => o.Item2.Air))
               return Distance + 1;
            char back = InvertDir[dir];
            dir = options
               .OrderByDescending(o => o.Item2.Distance)
               .Select(o => o.Item1)
               .First();
            if (options.Length > 2) {
               foreach (char alt in options
                     .OrderByDescending(o => o.Item2.Distance)
                     .Where(o => o.Item1 != dir)
                     .SkipLast(1)
                     .Select(o => o.Item1))
                  forks.Push((X, Y, alt));
            }
            if (back == dir) {
               // Backtrack to last fork
               (int, int, char) fork = forks.Pop();
               while (fork.Item1 != X || fork.Item2 != Y) {
                  dir = options
                     .OrderBy(o => o.Item2.Distance)
                     .Select(o => o.Item1)
                     .First();
                  await Move(dir);
                  Distance--;
                  options = await Look();
                  PaintCursor();
               }
               dir = fork.Item3;
            }
            await Move(dir);
            Distance++;
            options = await Look();
            PaintCursor();
         }
      }

      private async Task Move(char dir) {
         await IO(DirToIns[dir]);
         X = dir == 'h' ? X - 1 : dir == 'l' ? X + 1 : X;
         Y = dir == 'k' ? Y - 1 : dir == 'j' ? Y + 1 : Y;
      }

      private async Task<(char, Location)[]> Look() {
         List<(char, Location)> open = new List<(char, Location)>();
         foreach (char dir in DirToIns.Keys) {
            Location location = await Test(dir);
            if (location != null) {
               open.Add((dir, location));
               await Move(InvertDir[dir]);
            }
         }
         return open.ToArray();
      }

      private Location See() {
         Location location = Seen.FirstOrDefault(l => l.X == X && l.Y == Y);
         if (location == null) {
            location = new Location(X, Y, Distance + 1);
            if (Display[Y][X] == 'X')
               location.Air = true;
            Seen.Add(location);
         }
         return location;
      }

      private async Task<Location> Test(char dir) {
         long val = await IO(DirToIns[dir]);

         int tx = dir == 'h' ? X - 1 : dir == 'l' ? X + 1 : X;
         int ty = dir == 'k' ? Y - 1 : dir == 'j' ? Y + 1 : Y;
         if (val == 0) {
            Paint(tx, ty, '#');
            Display[ty][tx] = '#';
            return null;
         }
         else {
            X = tx;
            Y = ty;
            Display[Y][X] = val == 1 ? '·' : 'X';
            Location location = See();
            Paint(X, Y, Display[Y][X]);
            return location;
         }
      }

      private async void Play() {
         await Look();
         while (true) {
            while (true) {
               char action = Console.ReadKey(true).KeyChar;
               if (DirToIns.ContainsKey(action)) {
                  Dir = action;
                  break;
               }
            }
            await Test(Dir);
            await Look();
            PaintCursor();
         }
      }

      private void Paint(int x, int y, char pix) {
         if (!Playing && !Displaying)
            return;
         Console.SetCursorPosition(x, Console.BufferHeight - HEIGHT + y);
         Console.Write(pix);
      }

      private void PaintCursor() {
         if (!Playing && !Displaying)
            return;
         Console.SetCursorPosition(X, Console.BufferHeight - HEIGHT + Y);
         Console.Write('•');
         Console.SetCursorPosition(0, Console.BufferHeight - 1);
      }

      private void OnOutput(long val) {
         if (IOCompletionSource.Task.IsCompleted)
            NextInput = -1;
         else
            IOCompletionSource.SetResult(val);
      }

      private long WantsInput() {
         return NextInput;
      }

      public override string A() {
         IntcodeComputer brain = new IntcodeComputer(Input);
         brain.OnOutput += OnOutput;
         brain.WantsInput += WantsInput;
         if (Playing || Displaying) {
            if (Console.BufferHeight < HEIGHT) {
               Console.Write($"The buffer must be at least {HEIGHT} lines - ");
               Console.WriteLine($"currently {Console.BufferHeight} lines");
               return "";
            }
            // Clear playing area
            for (int i = 0; i < Console.BufferHeight - HEIGHT; i++)
               Console.WriteLine();
            Console.WriteLine($" {new string('-', WIDTH)} ");
            for (int i = 0; i < HEIGHT; i++)
               Console.WriteLine($"|{new string(' ', WIDTH)}|");
            Console.WriteLine($" {new string('-', WIDTH)} ");
            PaintCursor();
         }
         int distance = 0;
         if (Playing) {
            Play();
            brain.Start();
         }
         else {
            Task<int> task = Explore();
            brain.Start();
            task.Wait();
            distance = task.Result;
         }
         return distance.ToString();
      }

      public override string B() {
         return "";
      }

      private class Location {
         public Location(int x, int y, int distance) {
            X = x;
            Y = y;
            Distance = distance;
         }

         public bool Air { get; set; }
         public int X { get; private set; }
         public int Y { get; private set; }
         public int Distance { get; private set; }
      }
   }
}
