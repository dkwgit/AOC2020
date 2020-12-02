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
            var day1Answer = day1.Part1;
            Debug.Assert(day1Answer == "935419");
            var day2Answer = day1.Part2;
            Debug.Assert(day2Answer == "49880012");
        }
    }
}
