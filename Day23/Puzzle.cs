namespace AOC2020.Day23
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        private List<int> _cups;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "23";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                List<int> part1List = new List<int>(_cups);
                part1List = RunGame(part1List, 100, 1, _logger);

                int oneIndex = part1List.FindIndex(x => x == 1);
                for (int i = 0; i < oneIndex; i++)
                {
                    part1List.Add(part1List[0]);
                    part1List.RemoveAt(0);
                }

                Debug.Assert(part1List[0] == 1, "Expecting label 1 cup to be leftmost");
                string answer = string.Join(string.Empty, part1List.Skip(1).Select(x => x.ToString()).ToList());

                _logger.LogInformation("{Day}/Part1: Found {answer}", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                int maxNumber = 1000000;

                List<int> part2List = new List<int>(_cups);

                int max = _cups.Max();

                for (int j = part2List.Count; j < maxNumber; j++)
                {
                    part2List.Add(max + 1);
                    max++;
                }

                part2List = RunGame(part2List, maxNumber, 10, _logger);

                int indexOf1Value = part2List.FindIndex(x => x == 1);

                string answer = (part2List[indexOf1Value + 1] * 1L * part2List[indexOf1Value + 2]).ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer}", Day, answer);
                return answer;
            }
        }

        public static List<int> RunGame(List<int> cupList, int rounds, int superRounds, ILogger logger)
        {
            int currentCupIndex = 0;
            List<int> removeCups = new ();

            int maxOfList = cupList.Max();
            List<int> maxList = new () { maxOfList - 3, maxOfList - 2, maxOfList - 1, maxOfList };
            List<int> minList = new () { 1, 2, 3, 4 };

            for (int i = 0; i < rounds * superRounds; i++)
            {
                removeCups.AddRange(cupList.GetRange(currentCupIndex + 1, 3));
                cupList.RemoveRange(currentCupIndex + 1, 3);
                int max = maxList.Where(x => !removeCups.Contains(x)).Max();
                int min = minList.Where(y => !removeCups.Contains(y)).Min();
                int value = cupList[currentCupIndex] - 1;

                bool looking = true;
                int index = -1;

                while (looking)
                {
                    if (value < min)
                    {
                        value = max;
                    }

                    index = cupList.FindIndex(c => c == value);
                    if (index == -1)
                    {
                        value -= 1;
                    }
                    else
                    {
                        looking = false;
                        break;
                    }
                }

                cupList.InsertRange(index + 1, removeCups);
                removeCups.Clear();
                cupList.Add(cupList[0]);
                cupList.RemoveAt(0);

                if (i > 0 && (i + 1) % rounds == 0)
                {
                    logger.LogInformation("at index {i}, element 1 is {element1} and element 2 is {element2}", i, cupList[1], cupList[2]);
                }
            }

            return cupList;
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;

            _cups = _input[0].ToArray().Select(x => int.Parse(x.ToString())).ToList();
        }
    }
}
