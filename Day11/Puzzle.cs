namespace AOC2020.Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Map;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly Dictionary<Type, char> _mapSquaresToText = new ();

        private List<string> _input = null;

        private Map _waitingRoom = null;

        private List<string> _currentRepresentation = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;

            _mapSquaresToText.Add(typeof(AOC2020.Map.EmptyValue), '.');
            _mapSquaresToText.Add(typeof(EmptySeat), 'L');
            _mapSquaresToText.Add(typeof(FullSeat), '#');
        }

        public string Day => "11";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                _currentRepresentation = _waitingRoom.GetTextRepresentation(_mapSquaresToText);

                /* var initial = new Printer(_currentRepresentation);
                initial.Print(); */

                bool different = true;
                while (different)
                {
                    RepresentationComparer r = new RepresentationComparer(_currentRepresentation);
                    _waitingRoom.ChangeSquares(MutateSquareForPart1);
                    var representation = _waitingRoom.GetTextRepresentation(_mapSquaresToText);

                    /* var printer = new Printer(representation);
                    printer.Print(); */

                    different = r.Different(representation);
                    _currentRepresentation = representation;
                }

                var flatten = string.Join(string.Empty, _currentRepresentation.ToArray());
                string answer = flatten.Count(x => x == '#').ToString();

                _logger.LogInformation("{Day}/Part1: Found {answer} occupied seats after waiting room stabilizes", Day, answer);
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
            _waitingRoom = new MapBuilder<EmptySeat>(_input, false).Build();
        }

        private ISquareValue MutateSquareForPart1(Square s)
        {
            var neighbors = s.GetNeighbors();
            if (s.Has(typeof(EmptySeat)))
            {
                if (!neighbors.Any(x => x.Has(typeof(FullSeat))))
                {
                    return new FullSeat();
                }
            }

            if (s.Has(typeof(FullSeat)))
            {
                if (neighbors.Where(x => x.Has(typeof(FullSeat))).Count() >= 4)
                {
                    return new EmptySeat();
                }
            }

            return s.Value;
        }

        /*private ISquareValue MutateSquareForPart2(Square s)
        {
            var neighbors = s.GetNeighbors();
            if (s.Has(typeof(EmptySeat)))
            {
                if (!neighbors.Any(x => x.Has(typeof(FullSeat))))
                {
                    return new FullSeat();
                }
            }

            if (s.Has(typeof(FullSeat)))
            {
                if (neighbors.Where(x => x.Has(typeof(FullSeat))).Count() >= 4)
                {
                    return new EmptySeat();
                }
            }

            return s.Value;
        }*/
    }
}
