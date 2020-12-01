using System;
using System.Collections.Generic;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = AOC2020Library.PuzzleInputStore.GetPuzzleInputList("1");
            string puzzleOneAnswer = PuzzleOne(input);
            Console.WriteLine(puzzleOneAnswer);
        }

        static string PuzzleOne(List<string> input)
        {
            Dictionary<int, int> puzzleItems = new Dictionary<int, int>();
            string answer = "";

            int turn = 1;
            foreach (var item in input)
            {
                int number = int.Parse(item);
                int pairedNumber = 2020 - number;
                if (puzzleItems.ContainsKey(pairedNumber))
                {
                    answer = $"Found on turn {turn}, pair is {number},{pairedNumber}, which multiplied equal: {number * pairedNumber}";
                    break;
                }
                else
                {
                    puzzleItems.Add(number, 1);
                }
                turn++;
            }
            return answer;
        }
    }
}
