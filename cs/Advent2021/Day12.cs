using System.Collections.Generic;

#pragma warning disable CA1310

namespace AdventOfCode.Advent2021 {
   public class Day12 : AdventDay {
      public override int Day => 12;
      public override int Year => 2021;

      private Dictionary<string, List<string>> _caves;
      private Dictionary<string, List<string>> Caves {
         get {
            if (_caves == null) {
               void AddCave(string[] parts) {
                  if (_caves.ContainsKey(parts[0]))
                     _caves[parts[0]].Add(parts[1]);
                  else
                     _caves.Add(parts[0], new List<string> { parts[1] });
                  if (_caves.ContainsKey(parts[1]))
                     _caves[parts[1]].Add(parts[0]);
                  else
                     _caves.Add(parts[1], new List<string> { parts[0] });
               }
               _caves = new Dictionary<string, List<string>>();
               foreach (string line in InputLines)
                  AddCave(line.Split('-'));
            }
            return _caves;
         }
      }

      private IEnumerable<string> FindPathsA(string path, string cave) {
         path = $"{path}-{cave}";
         if (cave == "end")
            yield return path;
         else
            foreach (string next in Caves[cave]) {
               if (char.IsUpper(next[0]) || !path.Contains($"-{next}-"))
                  foreach (string nextpath in FindPathsA(path, next))
                     yield return nextpath;
            }
      }

      public override string A() {
         List<string> paths = new List<string>();
         foreach (string cave in Caves["start"])
            paths.AddRange(FindPathsA("-start", cave));
         return paths.Count.ToString();
      }

      private IEnumerable<string> FindPathsB(string path, string cave) {
         path = $"{path}-{cave}";
         if (cave == "end")
            yield return path;
         else
            foreach (string next in Caves[cave]) {
               string findpath = path;
               if (next == "start")
                  continue;
               if (char.IsLower(next[0]) && path.Contains($"-{next}-"))
                  if (path[0] == '$')
                     continue;
                  else
                     findpath = $"${path}";
               foreach (string nextpath in FindPathsB(findpath, next))
                  yield return nextpath;
            }
      }

      public override string B() {
         List<string> paths = new List<string>();
         foreach (string cave in Caves["start"])
            paths.AddRange(FindPathsB("-start", cave));
         return paths.Count.ToString();
      }
   }
}
