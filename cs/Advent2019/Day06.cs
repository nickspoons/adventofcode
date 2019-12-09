using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Advent2019 {
   public class Day06 : AdventDay {
      public override int Day => 6;
      public override int Year => 2019;

      public Dictionary<string, IEnumerable<string>> CalculateOrbits() {
         Dictionary<string, string> orbits = new Dictionary<string, string>();
         foreach (string line in InputLines) {
            string[] parts = line.Split(')');
            orbits.Add(parts[1], parts[0]);
         }
         Dictionary<string, IEnumerable<string>> orbitLists =
            new Dictionary<string, IEnumerable<string>>();
         string[] os;
         IEnumerable<string> CountOrbits(string key) {
            if (orbitLists.ContainsKey(key))
               return orbitLists[key];
            if (!orbits.ContainsKey(orbits[key])) {
               os = new[] { orbits[key] };
               orbitLists[key] = os;
               return os;
            }
            os = CountOrbits(orbits[key]).Append(orbits[key]).ToArray();
            orbitLists[key] = os;
            return os;
         }

         foreach (string key in orbits.Keys)
            CountOrbits(key);

         return orbitLists;
      }

      public override string A() {
         return CalculateOrbits().Values.Sum(ol => ol.Count()).ToString();
      }

      public override string B() {
         Dictionary<string, IEnumerable<string>> orbitLists = CalculateOrbits();
         List<string> you = orbitLists["YOU"].ToList();
         List<string> san = orbitLists["SAN"].ToList();
         while (you[0] == san[0]) {
            you.RemoveAt(0);
            san.RemoveAt(0);
         }
         return (you.Count + san.Count).ToString();
      }
   }
}
