﻿namespace AOC2020.Day22
{
    using System.Collections.Generic;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        private Hand _startingHandOne;

        private Hand _startingHandTwo;

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
                Setup();
                Game game = new (_startingHandOne, _startingHandTwo, new PartOneRuleVariants());
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
                Setup();
                Game game = new (_startingHandOne, _startingHandTwo, new PartTwoRuleVariants());
                game.Play();
                string answer = game.Score.ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} as the score of the winning Deck", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
        }

        private void Setup()
        {
            string state = string.Empty;

            int[] handOne = new int[25];
            int handOneCount = 0;
            int[] handTwo = new int[25];
            int handTwoCount = 0;

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
                        handOne[handOneCount++] = card;
                    }
                    else
                    {
                        handTwo[handTwoCount++] = card;
                    }
                }
            }

            _startingHandOne = Hand.DealHand(handOne);
            _startingHandTwo = Hand.DealHand(handTwo);
        }
    }
}
