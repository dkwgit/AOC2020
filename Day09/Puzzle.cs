﻿namespace AOC2020.Day09
{
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Combinatorics.Collections;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly List<long> _codes = new ();

        private List<string> _input = null;

        private int _indexOFWeakNumber = -1;

        private long _weakNumber = -1;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "09";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                string answer = string.Empty;
                int total = _codes.Count;
                int start = 0;
                int countTake = 25;
                int iteration = 25;

                while (iteration < total)
                {
                    Combinations<long> combi = new Combinations<long>(_codes.Skip(start).Take(countTake).ToArray(), 2);
                    if (!combi.Any(x => x[0] + x[1] == _codes[iteration]))
                    {
                        _weakNumber = _codes[iteration];
                        _indexOFWeakNumber = iteration;
                        answer = _codes[iteration].ToString();
                        break;
                    }

                    start++;
                    iteration++;
                }

                _logger.LogInformation("{Day}/Part1: Found {answer} as the weak number", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = string.Empty;
                int start = 0;
                bool found = false;

                for (int take = 2; take < _indexOFWeakNumber; take++)
                {
                    start = 0;
                    while (start + take <= _indexOFWeakNumber)
                    {
                        if (_codes.Skip(start).Take(take).Sum(x => x) == _weakNumber)
                        {
                            long min = _codes.Skip(start).Take(take).Min();
                            long max = _codes.Skip(start).Take(take).Max();
                            long both = min + max;
                            answer = both.ToString();
                            found = true;
                            break;
                        }

                        start++;
                    }

                    if (found)
                    {
                        break;
                    }
                }

                _logger.LogInformation("{Day}/Part2: Found {answer} as the min and max of a contiguous range that sums to the weak number", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
            foreach (var item in input)
            {
                _codes.Add(long.Parse(item));
            }
        }
    }
}
