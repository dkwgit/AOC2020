namespace AOC2020.Day17
{
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private const int _numberOfTurns = 6;

        private readonly ILogger _logger;

        private int _width;

        private int _length;

        private int _height;

        private int[,,] _space = null;

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "17";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                for (int turn = 0; turn < _numberOfTurns; turn++)
                {
                    DoTurn();
                }

                string answer = CountActive().ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer}", Day, answer);
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

            // retrieve input and measure as rows and columns
            List<List<int>> rows = new ();
            foreach (var line in input)
            {
                var row = new List<int>();
                rows.Add(row);
                foreach (var c in line)
                {
                    row.Add(c == '#' ? 1 : 0);
                }
            }

            int rowCount = rows.Count;
            int columnCount = rows.Max(x => x.Count);

            // figiure enough room to expand outward as we go through successive turns (allowing extra room for distance one past the number of turn
            // basically put the input as a center and surround it with 8 tiles of width enough to handle expansion during turns.
            int tileWidth = columnCount >= (_numberOfTurns + 1) ? columnCount : (_numberOfTurns + 1);
            int tileLength = rowCount > (_numberOfTurns + 1) ? rowCount : (_numberOfTurns + 1);

            _width = tileWidth + columnCount + tileWidth;
            _length = tileLength + rowCount + tileLength;

            // enough to go vertically up by turn + 1 and vertically down by same
            _height = 1 + ((_numberOfTurns + 1) * 2);

            _space = CreateSpace(_length, _width, _height);

            // Starting coordinates to write the initial layout
            int x = tileWidth;
            int y = tileLength;
            int z = _numberOfTurns + 1;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    _space[y + i, x + j, z] = rows[i][j];
                }
            }
        }

        private static int[,,] CreateSpace(int length, int width, int height)
        {
            return new int[length, width, height];
        }

        private int ActiveNeighborCount(int y, int x, int z)
        {
            int activeNeighbors = 0;

            // layer below
            activeNeighbors += GetLayerActiveCount(y, x, z - 1);

            // same layer
            activeNeighbors += GetLayerActiveCount(y, x, z, true);

            // layer above
            activeNeighbors += GetLayerActiveCount(y, x, z + 1);

            return activeNeighbors;
        }

        private int CountActive()
        {
            int activeCount = 0;

            for (int y = 0; y < _length; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    for (int z = 0; z < _height; z++)
                    {
                        if (_space[y, x, z] == 1)
                        {
                            activeCount++;
                        }
                    }
                }
            }

            return activeCount;
        }

        private int GetLayerActiveCount(int y, int x, int zz, bool skipMiddle = false)
        {
            int activeCount = 0;
            for (int yy = y - 1; yy < y + 2; yy++)
            {
                for (int xx = x - 1; xx < x + 2; xx++)
                {
                    if (skipMiddle)
                    {
                        if (yy == y && xx == x)
                        {
                            continue;
                        }
                    }

                    if (_space[yy, xx, zz] == 1)
                    {
                        activeCount++;
                    }
                }
            }

            return activeCount;
        }

        private void DoTurn()
        {
            int[,,] newSpace = new int[_length, _width, _height];

            // Start one from the edge, so each thing we evaluate has all 26 cube surrounding
            for (int y = 1; y < _length - 1; y++)
            {
                for (int x = 1; x < _width - 1; x++)
                {
                    for (int z = 1; z < _height - 1; z++)
                    {
                        int anc = ActiveNeighborCount(y, x, z);
                        int newValue = _space[y, x, z];

                        if (newValue == 1)
                        {
                            if (!(anc >= 2 && anc <= 3))
                            {
                                newValue = 0;
                            }
                        }
                        else
                        {
                            if (anc == 3)
                            {
                                newValue = 1;
                            }
                        }

                        newSpace[y, x, z] = newValue;
                    }
                }
            }

            _space = newSpace;
        }
    }
}
