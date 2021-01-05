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

        private readonly int[] _neighborVariance = new int[3] { -1, 0, 1 };

        private int _width;

        private int _length;

        private int _height;

        private int _extra;

        private int _estart;

        private int _dimensionCount;

        private int[,,,] _space = null;

        private List<string> _input = null;

        private List<int[]> _dimensionVarianceCombos;

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
                _dimensionCount = 3;
                ComboGenerator<int> gen = new ComboGenerator<int>(_neighborVariance, _dimensionCount);
                _dimensionVarianceCombos = new List<int[]>();

                foreach (var item in gen.Iterator())
                {
                    if (!item.All(x => x == 0))
                    {
                        _dimensionVarianceCombos.Add(item);
                    }
                }

                for (int turn = 0; turn < _numberOfTurns; turn++)
                {
                    DoTurn();
                }

                string answer = CountActive().ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} cubes active in 3 dimensional space", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                ProcessPuzzleInput(_input);

                _dimensionCount = 4;
                ComboGenerator<int> gen = new ComboGenerator<int>(_neighborVariance, _dimensionCount);
                _dimensionVarianceCombos = new List<int[]>();

                foreach (var item in gen.Iterator())
                {
                    if (!item.All(x => x == 0))
                    {
                        _dimensionVarianceCombos.Add(item);
                    }
                }

                for (int turn = 0; turn < _numberOfTurns; turn++)
                {
                    DoTurn();
                }

                string answer = CountActive().ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} cubes active in 4 dimensional space", Day, answer);
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

            _extra = _height;

            _space = CreateSpace(_length, _width, _height, _extra);

            // Starting coordinates to write the initial layout
            int xstart = tileWidth;
            int ystart = tileLength;
            int zstart = _numberOfTurns + 1;
            int estart = _estart = _numberOfTurns + 1;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    _space[ystart + i, xstart + j, zstart, estart] = rows[i][j];
                }
            }
        }

        private static int[,,,] CreateSpace(int length, int width, int height, int extra)
        {
            return new int[length, width, height, extra];
        }

        private int ActiveNeighborCount(int y, int x, int z, int e)
        {
            int activeNeighbors = 0;

            foreach (var item in _dimensionVarianceCombos)
            {
                int yoffset = item[0];
                int xoffset = item[1];
                int zoffset = item[2];
                int eoffset = item.Length == 4 ? item[3] : 0;

                if (_space[y + yoffset, x + xoffset, z + zoffset, e + eoffset] == 1)
                {
                    activeNeighbors++;
                }
            }

            return activeNeighbors;
        }

        private int CountActive()
        {
            int activeCount = 0;
            int estart = _dimensionCount == 3 ? _estart : 0;
            int elimit = _dimensionCount == 3 ? _estart + 1 : _extra;

            for (int y = 0; y < _length; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    for (int z = 0; z < _height; z++)
                    {
                        for (int e = estart; e < elimit; e++)
                        {
                            if (_space[y, x, z, e] == 1)
                            {
                                activeCount++;
                            }
                        }
                    }
                }
            }

            return activeCount;
        }

        private void DoTurn()
        {
            List<Change> changes = new (100);

            int estart = _dimensionCount == 3 ? _estart : 1;
            int elimit = _dimensionCount == 3 ? _estart + 1 : _extra - 1;

            // Start one from the edge, so each thing we evaluate has all 26 cube surrounding
            int changeIndex = 0;
            for (int y = 1; y < _length - 1; y++)
            {
                for (int x = 1; x < _width - 1; x++)
                {
                    for (int z = 1; z < _height - 1; z++)
                    {
                        for (int e = estart; e < elimit; e++)
                        {
                            int anc = ActiveNeighborCount(y, x, z, e);
                            int newValue = _space[y, x, z, e];

                            if (newValue == 1)
                            {
                                if (!(anc >= 2 && anc <= 3))
                                {
                                    changes.Add(new Change() { Y = y, X = x, Z = z, E = e, NewValue = 0 });
                                    changeIndex++;
                                }
                            }
                            else
                            {
                                if (anc == 3)
                                {
                                    changes.Add(new Change() { Y = y, X = x, Z = z, E = e, NewValue = 1 });
                                    changeIndex++;
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < changeIndex; i++)
            {
                Change change = changes[i];
                _space[change.Y, change.X, change.Z, change.E] = change.NewValue;
            }
        }

        internal record Change
        {
            public int Y { get; init; }

            public int X { get; init; }

            public int Z { get; init; }

            public int E { get; init; }

            public int NewValue { get; init; }
        }
    }
}
