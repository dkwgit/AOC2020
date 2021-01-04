namespace AOC2020.Day03
{
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Map;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly List<Point> _part2Slopes = new ()
        {
            new Point(1, 1),
            new Point(3, 1),
            new Point(5, 1),
            new Point(7, 1),
            new Point(1, 2),
        };

        private List<string> _input = null;

        private Map _forest = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                Point slope = new Point(3, 1);
                List<Square> path = _forest.Run(slope);
                string answer = path.Where(x => x.Value == '#').Count().ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} trees while sledding through the forest on slope {slope}", Day, answer, slope);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                // overflowed an int first time around
                long answerNumber = 1;

                foreach (var slope in _part2Slopes)
                {
                    _forest.ResetMap(new Point(0, 0));
                    List<Square> path = _forest.Run(slope);
                    answerNumber *= path.Where(x => x.Value == '#').Count();
                }

                string answer = answerNumber.ToString();
                _logger.LogInformation("{Day}/Part2: Found multiplied tree counts for all slopes checked: {answer}", Day, answer);

                return answer;
            }
        }

        public string Day => "03";

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
            _forest = new MapBuilder(_input, true).Build();
        }
    }
}
