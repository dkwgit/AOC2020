namespace AOC2020.Day15
{
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly Dictionary<int, (int Penultimate, int Ultimate)> _numberTurnInfo = new ();

        private readonly List<int> _numbers = new ();

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
                Setup();
                int finalTurn = 2020;
                string answer = RunToTurn(finalTurn);
                _logger.LogInformation("{Day}/Part1: Found {answer} for turn {finalTurn}", Day, answer, finalTurn);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                Setup();
                int finalTurn = 30000000;
                string answer = RunToTurn(finalTurn);
                _logger.LogInformation("{Day}/Part2: Found {answer} for turn {finalTurn}", Day, answer, finalTurn);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
        }

        private void Setup()
        {
            _numbers.Clear();
            _numberTurnInfo.Clear();

            var result = _input[0].Split(",").Select(x => int.Parse(x)).ToList();

            for (int turn = 0; turn < result.Count; turn++)
            {
                _numbers.Add(result[turn]);
                _numberTurnInfo.Add(result[turn], (Penultimate: turn, Ultimate: turn));
            }
        }

        private string RunToTurn(int finalTurn)
        {
            for (int turn = _numbers.Count; turn < finalTurn; turn++)
            {
                var priorNumber = _numbers[turn - 1];
                (int penultimate, int ultimate) = _numberTurnInfo[priorNumber];

                int turnDiffOfPrior = ultimate - penultimate;

                int newNumber = turnDiffOfPrior;

                if (_numberTurnInfo.ContainsKey(newNumber))
                {
                    (_, int oldUltimate) = _numberTurnInfo[newNumber];
                    _numberTurnInfo[newNumber] = (Penultimate: oldUltimate, Ultimate: turn);
                }
                else
                {
                    _numberTurnInfo.Add(newNumber, (Penultimate: turn, Ultimate: turn));
                }

                _numbers.Add(newNumber);
            }

            return _numbers[finalTurn - 1].ToString();
        }
    }
}
