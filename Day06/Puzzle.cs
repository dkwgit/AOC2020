namespace AOC2020.Day06
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        private List<string> _groups = new();
        private List<int> _peopleInGroups = new();

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                string answer = string.Empty;
                List<int> questionCountsPerGroup = new();
                Dictionary<char, int> questionsInGroup = new();

                foreach (var group in _groups)
                {
                    for (int i = 0; i < group.Length; i++)
                    {
                        if (!questionsInGroup.ContainsKey(group[i]))
                        {
                            questionsInGroup[group[i]] = 1;
                        }
                        else
                        {
                            questionsInGroup[group[i]] = questionsInGroup[group[i]] + 1;
                        }
                    }
                    questionCountsPerGroup.Add(questionsInGroup.Keys.Count);
                    questionsInGroup.Clear();
                }
                int totalQuestions = questionCountsPerGroup.Sum(x => x);
                answer = totalQuestions.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} total unique questions per group, summed by all groups", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = string.Empty;
                Dictionary<char, int> questionsInGroup = new();
                List<int> allQuestionsAnswered = new();

                int iGroup = 0;
                foreach (var group in _groups)
                {
                    for (int i = 0; i < group.Length; i++)
                    {
                        if (!questionsInGroup.ContainsKey(group[i]))
                        {
                            questionsInGroup[group[i]] = 1;
                        }
                        else
                        {
                            questionsInGroup[group[i]] = questionsInGroup[group[i]] + 1;
                        }
                    }

                    allQuestionsAnswered.Add(questionsInGroup.Values.Where(x => x == _peopleInGroups[iGroup]).ToList().Count);
                    questionsInGroup.Clear();
                    iGroup++;
                }

                int totalQuestions = allQuestionsAnswered.Sum(x => x);
                answer = totalQuestions.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} total all questions answered by group, summed by all groups", Day, answer);
                return answer;
            }
        }

        public string Day => "06";

        public void ProcessPuzzleInput()
        {
            _input = new PuzzleDataStore().GetPuzzleInputAsList(Day, false);

            StringBuilder group = new StringBuilder();
            int peopleInGroup = 0;

            for (int index = 0; index < _input.Count; index++)
            {
                string line = _input[index].Trim();

                if (!string.IsNullOrEmpty(line))
                {
                    peopleInGroup++;
                    group.Append(line);
                }

                if (string.IsNullOrEmpty(line) || index + 1 >= _input.Count)
                {
                    _groups.Add(group.ToString());
                    _peopleInGroups.Add(peopleInGroup);
                    peopleInGroup = 0;
                    group = new StringBuilder();
                }
            }
        }
    }
}
