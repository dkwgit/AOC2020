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

        private readonly List<(string name, int ruleNumber, (int, int) rangeA, (int, int) rangeB, int fieldNumber)> _rules = new ();

        private readonly List<int> _yourTicket = new ();

        private readonly List<List<int>> _tickets = new ();

        private readonly List<List<int>> _goodTickets = new ();

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
                    bool good = true;
                    foreach (var number in ticket)
                    {
                        if (_rules.All(rule => !(rule.rangeA.Item1 <= number && number <= rule.rangeA.Item2) && !(rule.rangeB.Item1 <= number && number <= rule.rangeB.Item2)))
                        {
                            badValues.Add(number);
                            good = false;
                        }
                    }

                    if (good)
                    {
                        _goodTickets.Add(ticket);
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
                List<List<int>> fieldValues = new ();
                for (int i = 0; i < _rules.Count; i++)
                {
                    List<int> values = new ();
                    fieldValues.Add(values);
                }

                // gather the values from each field [column] of each ticket. Each fieldValue list will have all the values from that field of all the tickets. E.g. all values from 0th field.
                foreach (var ticket in _goodTickets)
                {
                    int field = 0;

                    foreach (var number in ticket)
                    {
                        fieldValues[field].Add(number);
                        field++;
                    }
                }

                // Each field will satisfy one of more rules. Which rules does a field satisfy?
                List<(int field, List<int> rules)> fieldToRules = new ();

                // For each field, find all the rules that are satisfied for it (at least one rule per field)
                for (int j = 0; j < fieldValues.Count; j++)
                {
                    fieldToRules.Add((j, new List<int>()));
                    var values = fieldValues[j]; // The values for the jth field

                    for (int k = 0; k < _rules.Count; k++)
                    {
                        var rule = _rules[k];
                        if (values.All(x => (rule.rangeA.Item1 <= x && x <= rule.rangeA.Item2) || (rule.rangeB.Item1 <= x && x <= rule.rangeB.Item2)))
                        {
                            fieldToRules[j].rules.Add(rule.ruleNumber);
                        }
                    }
                }

                // Order ascending. The very first will only be satisfied by one rule, the second by two, etc.
                // We assign the rule for the first. That leaves only one viable rule for the second. That leaves only one viable rule for the third.
                foreach ((int field, List<int> rules) in fieldToRules.OrderBy(x => x.rules.Count))
                {
                    // If we have done this right, there is only a single un-assigned rule (fieldValue == -1).
                    var ruleNumber = rules.Where(x => _rules.Any(r => r.ruleNumber == x && r.fieldNumber == -1)).Single();
                    var ruleToAssign = _rules[ruleNumber];

                    // Change the field number of this rule to field, not -1
                    _rules[ruleToAssign.ruleNumber] = (ruleToAssign.name, ruleToAssign.ruleNumber, ruleToAssign.rangeA, ruleToAssign.rangeB, field);
                }

                var departureFields = _rules.Where(x => x.name.Contains("departure")).Select(x => x.fieldNumber);
                long answerValue = 1;
                foreach (var field in departureFields)
                {
                    answerValue *= _yourTicket[field];
                }

                string answer = answerValue.ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} of multipled values in the departure fields of your ticket", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;

            string rulePattern = @"^([^:]+):\s(\d+)-(\d+)\sor\s(\d+)-(\d+)$";
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
                _rules.Add((name, _rules.Count, (rangeA1, rangeA2), (rangeB1, rangeB2), -1));
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
