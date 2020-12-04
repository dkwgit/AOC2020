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
                (int,int) slope = (3, 1);
                List<Square> path = _forest.Run(slope);
                String answer = path.Where(x => x.HasTree).Count().ToString();
                Console.WriteLine($"Found {answer} trees sledding through the forest on slope {slope}");
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                _forest.ResetForest(new Point(0, 0));
                List<(int, int)> slopesToCheck = new()
                {
                    (1,1),
                    (3,1),
                    (5,1),
                    (7,1),
                    (1,2)
                };
                List<(int, (int, int))> slopeResults = new();
                foreach (var slope in slopesToCheck)
                {
                    List<Square> path = _forest.Run(slope);
                    slopeResults.Add((path.Where(x => x.HasTree).Count(), slope));
                    _forest.ResetForest(new Point(0, 0));
                }
                Int64 answerNumber = 1;
                foreach(var result in slopeResults)
                {
                    answerNumber *= result.Item1;
                }

                string answer = answerNumber.ToString();
                Console.WriteLine($"Trees multiplied for all the slopes checked are {answer}");
                return answer;
            }
        }

        public string Day => "03";

        public void SetInput(List<string> input)
        {
            _input = input;
            _forest = new ForestBuilder(input).Build();
        }
    }
}
