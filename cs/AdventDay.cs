using System;
using System.IO;
using System.Linq;
using System.Net;

namespace AdventOfCode {
   public abstract class AdventDay : IAdventDay {
      protected string Input {
         get {
            if (!File.Exists(InputFilename))
               DownloadInput();
            return File.ReadAllText(InputFilename);
         }
      }

      protected int[] InputIntLines => InputLines.Select(int.Parse).ToArray();
      protected string[] InputLines => Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

      public bool InputExists => File.Exists(InputFilename);
      public string InputFilename => $"Advent{Year}/Day{Day:00}-input";

      public abstract int Day { get; }
      public abstract int Year { get; }

      public abstract string A();
      public abstract string B();

      public void DownloadInput() {
         Console.WriteLine($"Downloading {Year}/{Day} now");

         string url = $"https://adventofcode.com/{Year}/day/{Day}/input";
         string sessionCookie = File.ReadAllText("session_cookie");
         using (WebClient client = new WebClient()) {
            client.Headers.Add("Cookie", $"session={sessionCookie}");
            client.DownloadFile(url, InputFilename);
         }
      }
   }
}
