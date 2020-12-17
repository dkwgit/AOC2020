namespace AOC2020.Day16
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly List<(string name, (int, int) rangeA, (int, int) rangeB)> _rules = new ();

        private readonly List<int> _yourTicket = new ();

        private readonly List<List<int>> _tickets = new ();

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "16";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                List<int> badValues = new ();
                foreach (var ticket in _tickets)
                {
                    foreach (var number in ticket)
                    {
                        if (_rules.All(rule => !(number >= rule.rangeA.Item1 && number <= rule.rangeA.Item2) && !(number >= rule.rangeB.Item1 && number <= rule.rangeB.Item2)))
                        {
                            badValues.Add(number);
                        }
                    }
                }

                string answer = badValues.Sum().ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as ticket scanning error rate", Day, answer);
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

            string rulePattern = @"(\w+\s?\w?):\s(\d+)-(\d+)\sor\s(\d+)-(\d+)";
            Regex regexRule = new Regex(rulePattern);

            string state = "field rules:";
            foreach (var item in input)
            {
                if (item != string.Empty)
                {
                    state = state switch
                    {
                        "waiting:" => item,
                        "field rules:" => ProcessRule(item, regexRule),
                        "your ticket:" => YourTicket(item),
                        "nearby tickets:" => Ticket(item),
                        _ => throw new System.Exception("unexpected value in switch expression"),
                    };
                }
                else
                {
                    state = "waiting:";
                }
            }
        }

        private string ProcessRule(string item, Regex regexRule)
        {
            var match = regexRule.Match(item);
            if (match.Success)
            {
                string name = match.Groups[1].Value;
                int rangeA1 = int.Parse(match.Groups[2].Value);
                int rangeA2 = int.Parse(match.Groups[3].Value);
                int rangeB1 = int.Parse(match.Groups[4].Value);
                int rangeB2 = int.Parse(match.Groups[5].Value);
                _rules.Add((name, (rangeA1, rangeA2), (rangeB1, rangeB2)));
            }
            else
            {
                throw new System.Exception($"Unexpected non match as rule: {item}");
            }

            return "field rules:";
        }

        private string YourTicket(string item)
        {
            var numbers = item.Split(',');
            foreach (var number in numbers)
            {
                _yourTicket.Add(int.Parse(number));
            }

            return "your ticket:";
        }

        private string Ticket(string item)
        {
            var numbers = item.Split(',');
            List<int> ticket = new ();

            foreach (var number in numbers)
            {
                ticket.Add(int.Parse(number));
            }

            _tickets.Add(ticket);

            return "nearby tickets:";
        }
    }
}
