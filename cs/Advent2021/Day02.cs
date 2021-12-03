namespace AdventOfCode.Advent2021 {
   public class Day02 : AdventDay {
      public override int Day => 2;
      public override int Year => 2021;

      public override string A() {
         int forward = 0;
         int depth = 0;
         foreach (string line in InputLines) {
            string direction = line.Split(' ')[0];
            int distance = int.Parse(line.Split(' ')[1]);
            if (direction == "forward")
               forward += distance;
            else
               depth += distance * (direction == "up" ? -1 : 1);
         }
         return (forward * depth).ToString();
      }

      public override string B() {
         int forward = 0;
         int depth = 0;
         int aim = 0;
         foreach (string line in InputLines) {
            string direction = line.Split(' ')[0];
            int n = int.Parse(line.Split(' ')[1]);
            if (direction == "forward") {
               forward += n;
               depth += aim * n;
            }
            else
               aim += n * (direction == "up" ? -1 : 1);
         }
         return (forward * depth).ToString();
      }
   }
}
