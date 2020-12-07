namespace AOC2020.Day07
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly Dictionary<Bag, int> _bagTypes = new ();

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "07";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                Bag shinyGold = _bagTypes.Where(x => x.Key.Name == "shiny gold").Select(x => x.Key).Single();
                int countOfProgenitors = _bagTypes.Keys.Count(x => x.IsProgenitor(shinyGold));
                string answer = countOfProgenitors.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} bag colors that ultimately can contain at least one shiny gold bag", Day, answer);

                return answer;
            }
        }

        public string Part2
        {
            get
            {
                Bag shinyGold = _bagTypes.Where(x => x.Key.Name == "shiny gold").Select(x => x.Key).Single();
                int countOfContainedBags = shinyGold.CountContainedBags(1);
                string answer = countOfContainedBags.ToString();

                _logger.LogInformation("{Day}/Part2: Found that a shiny gold bag untimatley contains {answer} bags, if every rule is followed.", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput()
        {
            _input = new PuzzleDataStore().GetPuzzleInputAsList(Day);

            string patternContains = @"^(\w+\s\w+)\sbags\scontain\s((\d)\s(\w+\s\w+)\sbags?[.,]\s?)+$";
            string pattenrContainsNo = @"^(\w+\s\w+)\sbags\scontain\sno\sother\sbags\.$";
            Regex regexContains = new Regex(patternContains);
            Regex regexContainsNo = new Regex(pattenrContainsNo);

            foreach (var line in _input)
            {
                string bagName = string.Empty;

                var match = regexContains.Match(line);
                if (match == Match.Empty)
                {
                    match = regexContainsNo.Match(line);
                    if (match == Match.Empty)
                    {
                        throw new System.Exception($"Did not a get a match on line {line}.");
                    }
                }

                bagName = match.Groups[1].Value;

                Bag bag;
                if (!_bagTypes.Keys.Any(x => x.Name == bagName))
                {
                    bag = new Bag(bagName);
                    _bagTypes.Add(bag, 1);
                }
                else
                {
                    bag = _bagTypes.Keys.Where(x => x.Name == bagName).Single();
                }

                if (match.Groups.Count > 2)
                {
                    Debug.Assert(match.Groups[3].Captures.Count == match.Groups[4].Captures.Count, "counts of number matches and bag name matches should be identical for child bags");
                    for (int i = 0; i < match.Groups[3].Captures.Count; i++)
                    {
                        int childBagCount = int.Parse(match.Groups[3].Captures[i].Value);
                        string childBagName = match.Groups[4].Captures[i].Value;

                        Bag childBag;
                        if (!_bagTypes.Keys.Any(x => x.Name == childBagName))
                        {
                            childBag = new Bag(childBagName);
                            _bagTypes.Add(childBag, 1);
                        }
                        else
                        {
                            childBag = _bagTypes.Keys.Where(x => x.Name == childBagName).Single();
                        }

                        bag.AddChild(childBag, childBagCount);
                        childBag.AddParent(bag);
                    }
                }
            }
        }
    }
}
