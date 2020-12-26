namespace AOC2020.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly List<Tile> _tiles = new ();

        private readonly Registry _registry = new ();

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "20";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                List<long> cornerTileIds = new ();
                foreach ((int key, List<(int tileId, string edge)> neighbors) in _registry.TileToNeighborTilesWithEdge)
                {
                    if (neighbors.Count == 4)
                    {
                        cornerTileIds.Add(key);
                    }
                }

                string answer = cornerTileIds.Aggregate((result, item) => result * item).ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer}", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                Picture picture = new ();
                picture.SetRegistry(_registry).
                SetLengths(_tiles[0].Length, _tiles[0].Length - 2, (int)Math.Sqrt(_tiles.Count)).
                Assemble().// picture.PrintPictureFromTiles();
                StripBorders();

                Pattern p = new ();

                int found = picture.FindPatterns(p, 1);
                if (found == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        // rotate
                        picture.RotateRight();
                        found = picture.FindPatterns(p, 1);
                        if (found == 1)
                        {
                            break;
                        }
                    }
                }

                if (found == 0)
                {
                    picture.FlipOnXAxis();

                    found = picture.FindPatterns(p, 1);
                    if (found == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            // rotate
                            picture.RotateRight();
                            found = picture.FindPatterns(p, 1);
                            if (found == 1)
                            {
                                break;
                            }
                        }
                    }
                }

                int totalFound = picture.FindPatterns(p);
                picture.PrintInColor();

                string answer = (picture.GetOccupiedPointCount() - picture.GetSerpentPointCount()).ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} as roughness of water after subtracting {totalFound} sea serpents", Day, answer, totalFound);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            int length = -1;
            int tileId = -1;
            int row = 0;
            char[,] tileData = null;

            _input = input;
            _input.Add(string.Empty); // need an ending newline to flush the final tile
            foreach (var line in _input)
            {
                if (line.Contains("Tile"))
                {
                    tileId = int.Parse(line[5..9]);
                    if (length == -1)
                    {
                        length = line.Length;
                    }
                    else
                    {
                        Debug.Assert(line.Length == length, "Expecting line length to be constant");
                    }

                    tileData = new char[length, length];
                }
                else if (line != string.Empty)
                {
                    Debug.Assert(line.Length == length, "Expecting line length to be constant");

                    for (int i = 0; i < length; i++)
                    {
                        tileData[row, i] = line[i];
                    }

                    row++;
                }
                else
                {
                    Debug.Assert(line == string.Empty, "Expecting empty line");

                    if (tileData != null)
                    {
                        Tile t = new Tile(_registry, tileId, length, tileData);
                        _tiles.Add(t);
                        _registry.Tiles.Add(t.Id, t);
                    }

                    tileId = -1;
                    row = 0;
                    tileData = null;
                }
            }
        }
    }
}
