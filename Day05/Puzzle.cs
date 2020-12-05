namespace AOC2020.Day05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;

    public class Puzzle : IPuzzle
    {
        private readonly List<BoardingPass> _boardingPasses = new ();

        private List<string> _input = null;

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                string answer = _boardingPasses.Max(x => x.SeatId).ToString();
                Console.WriteLine($"Found max seat {answer}");
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                int seatId = _boardingPasses.Where(x => _boardingPasses.Any(y => y.SeatId == x.SeatId + 2) && !_boardingPasses.Any(y => y.SeatId == x.SeatId + 1)).Single().SeatId + 1;
                Console.WriteLine($"Found seat {seatId}");
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
