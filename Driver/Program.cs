using System;
using System.Diagnostics;
using AOC2020Library;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1.Puzzle day1 = new Day1.Puzzle();
            day1.SetInput(PuzzleInputStore.GetPuzzleInputList("1"));
            var day1Answer1 = day1.Part1;
            Debug.Assert(day1Answer1 == "935419");
            var day1Answer2 = day1.Part2;
            Debug.Assert(day1Answer2 == "49880012");

            Day2.Puzzle day2 = new Day2.Puzzle();
            day2.SetInput(PuzzleInputStore.GetPuzzleInputList("2"));
            var day2Answer1 = day2.Part1;
            Debug.Assert(day2Answer1 == "600");
            var day2Answer2 = day2.Part2;
            Debug.Assert(day2Answer2 == "86");
        }
    }
}
