namespace AOC2020.Day19
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly Dictionary<int, string> _rules = new ();

        private readonly List<string> _expressions = new ();

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "19";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                List<string> validExpressions = new ();

                IAbstractRule rule = LoadRule(null, 0);

                foreach (var expression in _expressions)
                {
                    if (rule.Valid(expression))
                    {
                        validExpressions.Add(expression);
                    }
                }

                string answer = validExpressions.Count.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as number of expressions which are valid for rule 0", Day, answer);
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

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;

            string pattern = @"^(\d+):\s(.*)$";
            Regex regex = new Regex(pattern);

            foreach (var line in _input)
            {
                if (line != string.Empty && char.IsDigit(line[0]))
                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        int ruleId = int.Parse(match.Groups[1].Value);
                        string expression = match.Groups[2].Value;

                        _rules.Add(ruleId, expression);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unexpected failure of regex {pattern} match for line {line}");
                    }
                }
                else
                {
                    var lineValue = line.Trim();
                    if (lineValue != string.Empty)
                    {
                        _expressions.Add(lineValue);
                    }
                }
            }
        }

        internal IAbstractRule LoadRule(IAbstractRule parent, int ruleNumber)
        {
            string expression = _rules[ruleNumber];
            IAbstractRule rule = expression switch
            {
                string e when e.Contains(' ') && e.Contains('|') => AlternatingRule.Create(this, parent, ruleNumber, expression),
                string f when f.Contains(' ') && !f.Contains('|') => MultiRule.Create(this, parent, ruleNumber, expression),
                string g when g.Contains('"') => TerminalRule.Create(this, parent, ruleNumber, expression),
                string h when !h.Contains('"') && !h.Contains(' ') => SimpleRule.Create(this, parent, ruleNumber, expression),
                _ => throw new InvalidOperationException($"Unexpected expression {expression}"),
            };
            return rule;
        }
    }
}
