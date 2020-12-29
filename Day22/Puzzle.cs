namespace AOC2020.Day22
{
    using System.Collections.Generic;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly List<int> _playerOneDeck = new ();

        private readonly List<int> _playerTwoDeck = new ();

        private List<string> _input = null;

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
                while (_playerOneDeck.Count >= 1 && _playerTwoDeck.Count >= 1)
                {
                    int playerOneCard = _playerOneDeck[0];
                    _playerOneDeck.RemoveAt(0);
                    int playerTwoCard = _playerTwoDeck[0];
                    _playerTwoDeck.RemoveAt(0);

                    if (playerOneCard > playerTwoCard)
                    {
                        _playerOneDeck.Add(playerOneCard);
                        _playerOneDeck.Add(playerTwoCard);
                    }
                    else
                    {
                        _playerTwoDeck.Add(playerTwoCard);
                        _playerTwoDeck.Add(playerOneCard);
                    }
                }

                List<int> winningDeck = (_playerOneDeck.Count > 0) ? _playerOneDeck : _playerTwoDeck;
                int score = 0;
                for (int i = 1; i <= winningDeck.Count; i++)
                {
                    score += winningDeck[^i] * i;
                }

                string answer = score.ToString();
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
                        _playerOneDeck.Add(card);
                    }
                    else
                    {
                        _playerTwoDeck.Add(card);
                    }
                }
            }
        }
    }
}
