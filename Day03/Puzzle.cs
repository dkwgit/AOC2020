using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.Utilities;
using AOC2020.Sledding;

namespace AOC2020.Day03
{
    public class Puzzle : IPuzzle
    {
        private List<string> _input = null;
        private Forest _forest = null;

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                Tuple<int, int> slope = new Tuple<int, int>(3, 1);
                List<Square> path = _forest.Run(slope);
                String answer = path.Where(x => x.HasTree).Count().ToString();
                Console.WriteLine($"Found {answer} trees sledding through the forest on slope {slope}");
                return answer;
            }
        }

        public string Part2 => "";

        public string Day => "03";

        public void SetInput(List<string> input)
        {
            _input = input;
            _forest = new ForestBuilder(input).Build();
        }
    }
}
