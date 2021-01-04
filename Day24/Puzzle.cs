namespace AOC2020.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        private Tile _referenceTile;

        private TileManager _manager = new (10);

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "24";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                _referenceTile = _manager.GetTile(0, 0, 0);
                foreach (var line in _input)
                {
                    Tile.ProcessMoves(_referenceTile, line);
                }

                string answer = _manager.GetBlackTileCount().ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} black tiles after processing moves", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                for (int day = 0; day < 100; day++)
                {
                    _manager.DoTileFlipping();
                }

                string answer = _manager.GetBlackTileCount().ToString();

                _logger.LogInformation("{Day}/Part2: Found {answer} black tiles after 100 days", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
        }
    }
}
