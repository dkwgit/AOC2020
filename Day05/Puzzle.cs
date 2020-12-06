namespace AOC2020.Day05
{
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly List<BoardingPass> _boardingPasses = new ();

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                string answer = _boardingPasses.Max(x => x.SeatId).ToString();
                _logger.LogInformation("{Day}/Part1: Found max seats: {answer}", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                int seatId = _boardingPasses.Where(x => _boardingPasses.Any(y => y.SeatId == x.SeatId + 2) && !_boardingPasses.Any(y => y.SeatId == x.SeatId + 1)).Single().SeatId + 1;
                string answer = seatId.ToString();
                _logger.LogInformation("{Day}/Part2: Found seat id: {answer}", Day, answer);
                return seatId.ToString();
            }
        }

        public string Day => "05";

        public void ProcessPuzzleInput()
        {
            _input = new PuzzleDataStore().GetPuzzleInputAsList(Day, false);

            foreach (var line in _input)
            {
                _boardingPasses.Add(new BoardingPass(line));
            }
        }
    }
}
