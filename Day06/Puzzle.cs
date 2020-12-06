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

        private readonly List<GroupInfo> _groups = new ();

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
                string answer = string.Empty;

                int totalUniqueAnswerOccurrences = _groups.Sum(x => x.QuestionsWithAnAnswer);
                answer = totalUniqueAnswerOccurrences.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} total count of unique answered questions per group, summing that across all groups", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = string.Empty;

                int totalAnswersByAllGroupMembers = _groups.Sum(x => x.QuestionsAnsweredByAll);
                answer = totalAnswersByAllGroupMembers.ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} total count of all questions answered by all members of a group, summing that across all groups", Day, answer);
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

                // We finish a group either because of a blank line or end of file
                if (string.IsNullOrEmpty(line) || index + 1 >= _input.Count)
                {
                    _groups.Add(GetGroupInfo(group.ToString(), peopleInGroup));
                    peopleInGroup = 0;
                    group.Clear();
                }
            }
        }

        private static GroupInfo GetGroupInfo(string groupAnswers, int peopleInGroup)
        {
            Dictionary<char, int> questionsInGroup = new ();
            for (int i = 0; i < groupAnswers.Length; i++)
            {
                if (!questionsInGroup.ContainsKey(groupAnswers[i]))
                {
                    questionsInGroup[groupAnswers[i]] = 1;
                }
                else
                {
                    questionsInGroup[groupAnswers[i]] = questionsInGroup[groupAnswers[i]] + 1;
                }
            }

            return new GroupInfo(questionsInGroup, peopleInGroup);
        }
    }
}
