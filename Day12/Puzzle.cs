namespace AOC2020.Day12
{
    using System.Collections.Generic;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    using Point = AOC2020.Map.Point;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "12";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                Ship s = new Ship((1, 0), new Point(0, 0));
                foreach (var instruction in _input)
                {
                    s.ProcessInstruction(instruction);
                }

                string answer = s.ManhattanDistance.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} Manhattan distance after ship processed instructions", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = string.Empty;
                _logger.LogInformation("{Day}/Part2: Found {answer}", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
        }
    }
}
