namespace AOC2020.Day06
{
    using System.Collections.Generic;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                string answer = string.Empty;
                _logger.LogInformation("{Day}/Part1: Found", Day);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = string.Empty;
                _logger.LogInformation("{Day}/Part2: Found", Day);
                return answer;
            }
        }

        public string Day => "06";

        public void ProcessPuzzleInput()
        {
            _input = new PuzzleDataStore().GetPuzzleInputAsList(Day, false);
        }
    }
}
