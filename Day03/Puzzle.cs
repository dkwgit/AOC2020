using System;
using System.Collections.Generic;
using AOC2020Library;

namespace Day03
{
    public class Puzzle : IPuzzle
    {
        private List<string> _input = null;

        public List<string> Input => _input;

        public string Part1 => "";

        public string Part2 => "";

        public string Day => "03";

        public void SetInput(List<string> input)
        {
            _input = input;

        }
    }
}
