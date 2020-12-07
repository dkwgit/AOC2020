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

        private readonly BagManager _bagManager = new ();

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
                int countOfProgenitors = _bagManager.GetParentCount(_bagManager.GetBagByName("shiny gold"));

                string answer = countOfProgenitors.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} bag colors that ultimately can contain at least one shiny gold bag", Day, answer);

                return answer;
            }
        }

        public string Part2
        {
            get
            {
                Bag shinyGold = _bagManager.GetBagByName("shiny gold");
                int countOfContainedBags = shinyGold.CountContainedBags(1);
                string answer = countOfContainedBags.ToString();

                _logger.LogInformation("{Day}/Part2: Found that a shiny gold bag untimatley contains {answer} bags, if every rule is followed.", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput()
        {
            _input = new PuzzleDataStore().GetPuzzleInputAsList(Day);

            BagFactory factory = new BagFactory(_bagManager);

            foreach (var line in _input)
            {
                factory.ProcessLine(line);
            }
        }
    }
}
