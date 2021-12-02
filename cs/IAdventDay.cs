namespace AdventOfCode {
   public interface IAdventDay {
      bool InputExists { get; }
      string InputFilename { get; }

      int Day { get; }
      int Year { get; }

      string A();
      string B();
   }
}
