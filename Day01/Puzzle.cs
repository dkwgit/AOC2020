namespace AOC2020.Day01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;

    public class Puzzle : IPuzzle
    {
        private readonly SortedDictionary<int, int> _sortedInput = new ();

        private List<string> _input = null;

        public string Day => "01";

        public List<string> Input
        {
            get
            {
                return _input;
            }
        }

        public string Part1
        {
            get
            {
                string answer = string.Empty;

                int turn = 1;
                foreach (int number in _sortedInput.Keys)
                {
                    int pairedNumber = 2020 - number;

                    if (_sortedInput.ContainsKey(pairedNumber))
                    {
                        answer = $"{number * pairedNumber}";
                        Console.WriteLine($"Found on turn {turn}, pair is {number},{pairedNumber}, which multiplied equal: {answer}");
                        break;
                    }

                    turn++;
                }

                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = string.Empty;
                int turn = 1;

                foreach (int first in _sortedInput.Keys)
                {
                    foreach (int second in TripletFinder(first))
                    {
                        int third = 2020 - (first + second);
                        answer = $"{first * second * third}";
                        Console.WriteLine($"Found on turn {turn}, triplet is {first},{second},{third} which multiplied equal: {answer}");
                        turn++;
                        break;
                    }

                    if (string.Empty.CompareTo(answer) != 0)
                    {
                        break;
                    }
                }

                return answer;
            }
        }

        public void ProcessPuzzleInput()
        {
            _input = new PuzzleDataStore().GetPuzzleInputAsList(Day);
            for (int i = 0; i < _input.Count; i++)
            {
                _sortedInput.Add(int.Parse(_input[i]), i);
            }
        }

        private IEnumerable<int> TripletFinder(int first)
        {
            IEnumerable<int> list = _sortedInput.Keys.
                Where(
                    second => second > first &&
                    2020 - first - second > 0 &&
                    _sortedInput.ContainsKey(2020 - first - second)).
               Select(item => item);

            foreach (int item in list)
            {
                yield return item;
            }

            yield break;
        }
    }
}
