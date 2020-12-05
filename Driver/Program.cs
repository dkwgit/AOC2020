namespace AOC2020.Driver
{
    using System.Collections.Generic;
    using AOC2020.Utilities;

    public class Program
    {
        public static void Main()
        {
            RunPuzzleRegressionTests();

            /*Day05.Puzzle puzzle = new ();

            puzzle.ProcessPuzzleInput();

            string part1Answer = puzzle.Part1;

            string part2Answer = puzzle.Part2;*/
        }

        public static void RunPuzzleRegressionTests()
        {
            RegressionTesting.RunPuzzleRegressionTests(
                new List<IPuzzle>
                {
                    new AOC2020.Day01.Puzzle(),
                    new AOC2020.Day02.Puzzle(),
                    new AOC2020.Day03.Puzzle(),
                    new AOC2020.Day04.Puzzle(),
                    new AOC2020.Day05.Puzzle(),
                });
        }
    }
}
