namespace AOC2020.Day05
{
    using System.Collections.Generic;
    using AOC2020.Utilities;

    public class Puzzle : IPuzzle
    {
        private List<string> _input = null;

        public List<string> Input => _input;

        public string Part1 => throw new System.NotImplementedException();

        public string Part2 => throw new System.NotImplementedException();

        public string Day => "5";

        public void ProcessPuzzleInput()
        {
            _input = new PuzzleDataStore().GetPuzzleInputAsList(Day, false);
        }
    }
}
