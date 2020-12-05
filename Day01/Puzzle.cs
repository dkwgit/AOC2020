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
                (int, int) result = (from x in _sortedInput.Keys
                                     where x < (2020 - x) && _sortedInput.ContainsKey(2020 - x)
                                     select (x, 2020 - x)).Single();

                string answer = $"{result.Item1 * result.Item2}";
                Console.WriteLine($"Found pair is ({result.Item1}, {result.Item2}), which multiplied equal: {answer}");

                return answer;
            }
        }

        public string Part2
        {
            get
            {
                (int, int, int) triplet = TripletFinder();
                string answer = $"{triplet.Item1 * triplet.Item2 * triplet.Item3}";
                Console.WriteLine($"Found triplet ({triplet.Item1}, {triplet.Item2}, {triplet.Item3}) which multiplied equal: {answer}");

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

        private (int, int, int) TripletFinder()
        {
            var result =
                (from x in _sortedInput.Keys
                from y in _sortedInput.Keys
                where x < y && x + y < 2020
                from z in _sortedInput.Keys
                where y < z && x + y + z == 2020
                select (x, y, z)).Single();

            return result;
        }
    }
}
