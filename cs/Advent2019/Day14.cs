using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Advent2019 {
   public class Day14 : AdventDay {
      public Day14() {
         Reactions = new List<Reaction>();
         foreach (string line in InputLines) {
            string[] parts = line.Split(" => ");
            Match re = Regex.Match(parts[1], @"(\d+) (\a+)");
            Reaction reaction = new Reaction(parts[1]);
            foreach (string part in parts[0].Split(", "))
               reaction.Inputs.Add(new Resource(part));
            Reactions.Add(reaction);
         }
         Leftovers = Reactions.ToDictionary(r => r.Key, _ => 0);
      }

      public override int Day => 14;
      public override int Year => 2019;

      private readonly Dictionary<string, int> Leftovers;
      private readonly List<Reaction> Reactions;

      private int Requires(string key, int amount, int depth) {
         Reaction reaction = Reactions.First(r => r.Key == key);
         int making = reaction.Amount;
         int batches = 1;
         while (making < amount) {
            making += reaction.Amount;
            batches++;
         }
         Leftovers[key] += making - amount;

         int ore = 0;
         foreach (Resource input in reaction.Inputs) {
            int wanted = input.Amount * batches;
            if (input.Key == "ORE")
               ore += wanted;
            else {
               if (Leftovers[input.Key] >= wanted) {
                  Leftovers[input.Key] -= wanted;
                  wanted = 0;
               }
               else if (Leftovers[input.Key] > 0) {
                  wanted -= Leftovers[input.Key];
                  Leftovers[input.Key] = 0;
               }
               if (wanted > 0)
                  ore += Requires(input.Key, wanted, depth + 1);
            }
         }
         return ore;
      }

      public override string A() {
         return Requires("FUEL", 1, 0).ToString();
      }

      public override string B() {
         return "";
      }

      private class Reaction : Resource {
         public Reaction(string raw) : base(raw) { }
         public readonly List<Resource> Inputs = new List<Resource>();
      }

      private class Resource {
         public Resource(string raw) {
            Match re = Regex.Match(raw, @"(\d+) (\w+)");
            Key = re.Groups[2].Value;
            Amount = int.Parse(re.Groups[1].Value);
         }
         public int Amount { get; private set; }
         public string Key { get; private set; }
      }
   }
}
