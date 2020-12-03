using System;
using System.Collections.Generic;
using AOC2020.Utilities;

namespace AOC2020.Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            RunRegressionTests();

            /*Day03.Puzzle puzzle = new AOC2020.Day03.Puzzle();
            puzzle.SetInput(PuzzleDataStore.GetPuzzleInputList(puzzle.Day));
            string part1Answer = puzzle.Part1;
            string part2Answer = puzzle.Part2;*/
        }

        static void RunRegressionTests()
        {
            List<IPuzzle> list = new List<IPuzzle>
            {
                new AOC2020.Day01.Puzzle(),
                new AOC2020.Day02.Puzzle(),
                new AOC2020.Day03.Puzzle()
            };

            RegressionTests.RunRegressionTests(list);
        }
    }
}
