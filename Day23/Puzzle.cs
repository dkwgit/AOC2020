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
                List<int> finalList = RunGame(_cups, 100);

                int oneIndex = finalList.FindIndex(x => x == 1);
                for (int i = 0; i < oneIndex; i++)
                {
                    finalList.Add(finalList[0]);
                    finalList.RemoveAt(0);
                }

                Debug.Assert(finalList[0] == 1, "Expecting label 1 cup to be leftmost");
                string answer = string.Join(string.Empty, finalList.Skip(1).Select(x => x.ToString()).ToList());

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

        public static List<int> RunGame(List<int> cupList, int rounds)
        {
            List<int> localList = new List<int>(cupList);

            int currentCupIndex = 0;
            List<int> removeCups = new ();

            for (int i = 0; i < rounds; i++)
            {
                removeCups.AddRange(localList.GetRange(currentCupIndex + 1, 3));
                localList.RemoveRange(currentCupIndex + 1, 3);
                int min = localList.Min();
                int max = localList.Max();
                int value = localList[currentCupIndex] - 1;

                bool looking = true;
                int index = -1;

                while (looking)
                {
                    if (value < min)
                    {
                        value = max;
                    }

                    index = localList.FindIndex(c => c == value);
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

                localList.InsertRange(index + 1, removeCups);
                removeCups.Clear();
                localList.Add(localList[0]);
                localList.RemoveAt(0);
            }

            return localList;
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;

            _cups = _input[0].ToArray().Select(x => int.Parse(x.ToString())).ToList();
        }
    }
}
