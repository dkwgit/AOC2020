namespace AOC2020.Day01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly SortedDictionary<int, int> _sortedInput = new ();

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

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
                _logger.LogInformation("{Day}/Part1: Found pair is ({x}, {y}), which multiplied equal: {answer}", Day, result.Item1, result.Item2, answer);

                return answer;
            }
        }

        public string Part2
        {
            get
            {
                (int, int, int) triplet = TripletFinder();
                string answer = $"{triplet.Item1 * triplet.Item2 * triplet.Item3}";
                _logger.LogInformation("{Day}/Part2: Found triplet ({x}, {y}, {z}) which multiplied equal: {answer}", Day, triplet.Item1, triplet.Item2, triplet.Item3, answer);

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
