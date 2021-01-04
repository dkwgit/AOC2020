namespace AOC2020.Day15
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly List<int> _numbers = new ();

        private int[] _penultimates;

        private int[] _ultimates;

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "15";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                int finalTurn = 2020;
                Setup(finalTurn);
                string answer = RunToTurn(finalTurn);
                _logger.LogInformation("{Day}/Part1: Found {answer} for turn {finalTurn}", Day, answer, finalTurn);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                int finalTurn = 30000000;
                Setup(finalTurn);
                string answer = RunToTurn(finalTurn);
                _logger.LogInformation("{Day}/Part2: Found {answer} for turn {finalTurn}", Day, answer, finalTurn);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
        }

        private void Setup(int finalTurn)
        {
            _numbers.Clear();
            _ultimates = new int[finalTurn];
            _penultimates = new int[finalTurn];
            _ultimates.AsSpan().Fill(-1);

            var result = _input[0].Split(",").Select(x => int.Parse(x)).ToList();

            for (int turn = 0; turn < result.Count; turn++)
            {
                _numbers.Add(result[turn]);
                _penultimates[result[turn]] = turn;
                _ultimates[result[turn]] = turn;
            }
        }

        private string RunToTurn(int finalTurn)
        {
            int priorNumber = _numbers[^1];
            for (int turn = _numbers.Count; turn < finalTurn; turn++)
            {
                int newNumber = _ultimates[priorNumber] - _penultimates[priorNumber];

                _penultimates[newNumber] = _ultimates[newNumber] != -1 ? _ultimates[newNumber] : turn;
                _ultimates[newNumber] = turn;

                priorNumber = newNumber;
            }

            return priorNumber.ToString();
        }
    }
}
