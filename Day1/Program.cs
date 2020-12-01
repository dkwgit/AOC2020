using System;
using System.Collections.Generic;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = AOC2020Library.PuzzleInputStore.GetPuzzleInputList("1");
            Dictionary<int, int> puzzleItems = new Dictionary<int, int>();

            int turn = 1;
            foreach (var item in input)
            {
                int number = int.Parse(item);
                int pairedNumber = 2020 - number;
                if (puzzleItems.ContainsKey(pairedNumber))
                {
                    Console.WriteLine($"Found on turn {turn}, pair is {number},{pairedNumber}, which multiplied equal: {number * pairedNumber}");
                    break;
                }
                else
                {
                    puzzleItems.Add(number, 1);
                }
                turn++;
            }
        }
    }
}
