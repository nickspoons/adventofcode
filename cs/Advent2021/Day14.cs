using System.Collections.Generic;
using System.Linq;

#pragma warning disable IDE0008

namespace AdventOfCode.Advent2021 {
   public class Day14 : AdventDay {
      public override int Day => 14;
      public override int Year => 2021;

      private Dictionary<string, Rule> Rules;
      private Dictionary<char, long> Frequencies;
      private Dictionary<string, Dictionary<char, long>> FreqCache;
      private List<string> Template;

      private void ParseInput() {
         Dictionary<string, string> inRules = new Dictionary<string, string>();
         Frequencies = new Dictionary<char, long>();
         FreqCache = new Dictionary<string, Dictionary<char, long>>();
         Template = new List<string>();
         string template = InputLines[0];
         for (int i = 0; i < template.Length - 1; i++)
            Template.Add(template.Substring(i, 2));
         foreach (string line in InputLines.Skip(1)) {
            string[] parts = line.Split(" -> ");
            inRules.Add(parts[0], $"{parts[0][0]}{parts[1]}");
            char key = parts[1][0];
            if (!Frequencies.ContainsKey(key))
               Frequencies.Add(key, 0);
         }
         Rules = new Dictionary<string, Rule>();
         foreach (string key in inRules.Keys) {
            string a = inRules[key];
            string b = $"{a[1]}{key[1]}";
            char c = a[1];
            Rules.Add(key, new(a, b, c));
         }
         foreach (char c in template)
            Frequencies[c]++;
      }

      private void AddFrequencies(Dictionary<char, long> cached) {
         foreach (char key in cached.Keys)
            Frequencies[key] += cached[key];
      }

      private void ApplyRule(string key, int times) {
         if (times == 0)
            return;
         string cacheKey = $"{key}:{times}";
         if (FreqCache.ContainsKey(cacheKey))
            AddFrequencies(FreqCache[cacheKey]);
         else {
            var snapshot = Frequencies
               .ToDictionary(f => f.Key, f => f.Value);
            Rule r = Rules[key];
            Frequencies[r.c]++;
            ApplyRule(r.a, times - 1);
            ApplyRule(r.b, times - 1);
            FreqCache.Add(cacheKey, Frequencies
               .ToDictionary(f => f.Key, f => f.Value - snapshot[f.Key]));
         }
      }

      private void ApplyRules(int times) {
         foreach (string element in Template)
            ApplyRule(element, times);
      }

      private long CommonalityDifference() {
         var ordered = Frequencies.OrderBy(freq => freq.Value);
         var gfewest = ordered.First();
         var gmost = ordered.Last();
         return gmost.Value - gfewest.Value;
      }

      public override string A() {
         ParseInput();
         ApplyRules(10);
         return CommonalityDifference().ToString();
      }

      public override string B() {
         ParseInput();
         ApplyRules(40);
         return CommonalityDifference().ToString();
      }

      private record Rule(string a, string b, char c);
   }
}
