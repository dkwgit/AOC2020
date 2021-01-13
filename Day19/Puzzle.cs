namespace AOC2020.Day19
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly Dictionary<int, ReadOnlyMemory<char>> _rules = new ();

        private readonly List<ReadOnlyMemory<char>> _expressions = new ();

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
                IAbstractRule rule = LoadRule(null, 0);

                ConcurrentBag<bool> valids = new ();
                Parallel.ForEach(_expressions, expression =>
                {
                    valids.Add(GetValid(rule, expression));
                });

                string answer = valids.Count(x => x == true).ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as number of expressions which are valid for rule 0", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                _rules[8] = "42 | 42 8".AsMemory();
                _rules[11] = "42 31 | 42 11 31".AsMemory();

                IAbstractRule rule = LoadRule(null, 0);

                ConcurrentBag<bool> valids = new ();
                Parallel.ForEach(_expressions, expression =>
                {
                    valids.Add(GetValid(rule, expression));
                });

                string answer = valids.Count(x => x == true).ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} as number of expressions that are valid using recursive rules", Day, answer);
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

                        _rules.Add(ruleId, expression.AsMemory());
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
                        _expressions.Add(lineValue.AsMemory());
                    }
                }
            }
        }

        internal static bool GetValid(IAbstractRule rule, ReadOnlyMemory<char> expression)
        {
            return rule.Valid(expression.Span);
        }

        internal IAbstractRule LoadRule(IAbstractRule parent, int ruleNumber)
        {
            IAbstractRule rule = _rules[ruleNumber].Span switch
            {
                ReadOnlySpan<char> e when e.Contains(' ') && e.Contains('|') => AlternatingRule.Create(this, parent, ruleNumber, _rules[ruleNumber]),
                ReadOnlySpan<char> f when f.Contains(' ') && !f.Contains('|') => MultiRule.Create(this, parent, ruleNumber, _rules[ruleNumber]),
                ReadOnlySpan<char> g when g.Contains('"') => TerminalRule.Create(this, parent, ruleNumber, _rules[ruleNumber]),
                ReadOnlySpan<char> h when !h.Contains('"') && !h.Contains(' ') => SimpleRule.Create(this, parent, ruleNumber, _rules[ruleNumber]),
                _ => throw new InvalidOperationException($"Unexpected expression {_rules[ruleNumber].ToString()}"),
            };
            return rule;
        }
    }
}
