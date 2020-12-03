using System;
using System.Collections.Generic;
using AOC2020Library;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            RunRegressionTests();

            Day03.Puzzle puzzle = new Day03.Puzzle();
            puzzle.SetInput(PuzzleDataStore.GetPuzzleInputList(puzzle.Day));
            string part1Answer = puzzle.Part1;
            string part2Answer = puzzle.Part2;
        }

        static void RunRegressionTests()
        {
            List<IPuzzle> list = new List<IPuzzle>();
            list.Add(new Day01.Puzzle());
            list.Add(new Day02.Puzzle());
            //list.Add(new Day03.Puzzle());

            RegressionTests.RunRegressionTests(list);
        }
    }
}
