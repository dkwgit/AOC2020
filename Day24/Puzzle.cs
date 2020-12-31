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
                _referenceTile = new Tile((0, 0, 0), new Dictionary<(int x, int y, int z), Tile>());
                foreach (var line in _input)
                {
                    Tile.ProcessMoves(_referenceTile, line);
                }

                string answer = _referenceTile.AllTiles.Values.Count(x => x.Color == 'b').ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} black tiles after processing moves", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                // initial needs: One beyond current max rings for new whites to flip to black,
                // and a second, extra ring beyond max rings, so that the first extra has full neighbor count
                // Thereafter we can keep adding one ring at a time
                int maxRingNeeded = _referenceTile.AllTiles.Values.Max(x => x.Ring) + 2;
                for (int ring = 1; ring <= maxRingNeeded; ring++)
                {
                    EnsureRing(ring);
                }

                for (int day = 0; day < 100; day++)
                {
                    EnsureRing(++maxRingNeeded);

                    Dictionary<(int x, int y, int z), Tile> newTileCollection = new ();
                    foreach (var item in _referenceTile.AllTiles)
                    {
                        var tile = item.Value;
                        tile.CopyLocationAndColor(newTileCollection);
                    }

                    foreach (var tile in _referenceTile.AllTiles.Values.Where(x => x.Ring < maxRingNeeded - 1).ToList())
                    {
                        int neighborBlackTileCount = tile.GetNeigborBlackTileCount();
                        if (tile.Color == 'b' && (neighborBlackTileCount == 0 || neighborBlackTileCount > 2))
                        {
                            newTileCollection[(tile.X, tile.Y, tile.Z)].Flip();
                        }

                        if (tile.Color == 'w' && neighborBlackTileCount == 2)
                        {
                            newTileCollection[(tile.X, tile.Y, tile.Z)].Flip();
                        }
                    }

                    foreach (var tileCopy in newTileCollection.Values)
                    {
                        _referenceTile.AllTiles[(tileCopy.X, tileCopy.Y, tileCopy.Z)].Color = tileCopy.Color;
                    }

                    // _logger.LogInformation("Day {day} has {count} black tiles", day, _referenceTile.AllTiles.Values.Count(x => x.Color == 'b'));
                }

                string answer = _referenceTile.AllTiles.Values.Count(x => x.Color == 'b').ToString();

                _logger.LogInformation("{Day}/Part2: Found {answer} black tiles after 100 days", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
        }

        private void EnsureRing(int ring)
        {
            var currentTile = _referenceTile.AllTiles.ContainsKey((0 * ring, 1 * ring, -1 * ring)) ? _referenceTile.AllTiles[(0 * ring, 1 * ring, -1 * ring)] : new Tile((0 * ring, 1 * ring, -1 * ring), _referenceTile.AllTiles);
            for (int direction = 0; direction < 6; direction++)
            {
                for (int moveInOneDirection = 1; moveInOneDirection < ring; moveInOneDirection++)
                {
                    currentTile = direction switch
                    {
                        0 => currentTile.MoveEast(),
                        1 => currentTile.MoveSouthEast(),
                        2 => currentTile.MoveSouthWest(),
                        3 => currentTile.MoveWest(),
                        4 => currentTile.MoveNorthWest(),
                        5 => currentTile.MoveNorthEast(),
                        _ => throw new InvalidOperationException($"Unexpected direction {direction}"),
                    };
                }
            }
        }
    }
}
