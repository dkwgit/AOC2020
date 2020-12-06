namespace AOC2020.Day03
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Sledding;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        private Forest _forest = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                (int, int) slope = (3, 1);
                List<Square> path = _forest.Run(slope);
                string answer = path.Where(x => x.HasTree).Count().ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} trees while sledding through the forest on slope {slope}", Day, answer, slope);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                _forest.ResetForest(new Point(0, 0));

                List<(int, int)> slopesToCheck = new ()
                {
                    (1, 1),
                    (3, 1),
                    (5, 1),
                    (7, 1),
                    (1, 2),
                };

                List<(int, (int, int))> slopeResults = new ();

                foreach (var slope in slopesToCheck)
                {
                    List<Square> path = _forest.Run(slope);
                    slopeResults.Add((path.Where(x => x.HasTree).Count(), slope));
                    _forest.ResetForest(new Point(0, 0));
                }

                long answerNumber = 1;

                foreach (var result in slopeResults)
                {
                    answerNumber *= result.Item1;
                }

                string answer = answerNumber.ToString();
                _logger.LogInformation("{Day}/Part2: Found multiplied tree counts for all slopes checked: {answer}", Day, answer);

                return answer;
            }
        }

        public string Day => "03";

        public void ProcessPuzzleInput()
        {
            _input = new PuzzleDataStore().GetPuzzleInputAsList(Day, false);
            _forest = new ForestBuilder(_input).Build();
        }
    }
}
