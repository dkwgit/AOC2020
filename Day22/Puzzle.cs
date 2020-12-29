namespace AOC2020.Day22
{
    using System.Collections.Generic;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        private List<int> _startingDeckOne;

        private List<int> _startingDeckTwo;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "22";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                Game game = new (_startingDeckOne, _startingDeckTwo, new PartOneRuleVariants());
                game.Play();
                string answer = game.Score.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as the score of the winning Deck", Day, answer);
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
            string state = string.Empty;

            _startingDeckOne = new ();
            _startingDeckTwo = new ();

            foreach (var line in _input)
            {
                if (line.Contains("Player 1"))
                {
                    state = "Player 1";
                }
                else if (line.Contains("Player 2"))
                {
                    state = "Player 2";
                }

                if (int.TryParse(line, out int card))
                {
                    if (state == "Player 1")
                    {
                        _startingDeckOne.Add(card);
                    }
                    else
                    {
                        _startingDeckTwo.Add(card);
                    }
                }
            }
        }
    }
}
