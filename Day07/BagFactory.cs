namespace AOC2020.Day07
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    internal class BagFactory
    {
        private static readonly string _patternContains = @"^(\w+\s\w+)\sbags\scontain\s((\d)\s(\w+\s\w+)\sbags?[.,]\s?)+$";

        private static readonly string _pattenrContainsNo = @"^(\w+\s\w+)\sbags\scontain\sno\sother\sbags\.$";

        private static readonly Regex _regexContainsChildren = new Regex(_patternContains);

        private static readonly Regex _regexContainsNoChildren = new Regex(_pattenrContainsNo);

        private readonly BagManager _bagManager;

        public BagFactory(BagManager manager)
        {
            _bagManager = manager;
        }

        public Bag ProcessLine(string line)
        {
            Bag bag;

            Match match = MatchLine(line, out bool hasChildren);

            string bagName = GetBagName(match);

            bag = _bagManager.GetOrInsertBagByName(bagName, out bool _);

            if (hasChildren)
            {
                Debug.Assert(match.Groups.Count > 2, "Bags with children should have additional mataches in their regex");
                ProcessRelationships(bag, match);
            }

            return bag;
        }

        private static Match MatchLine(string line, out bool hasChildren)
        {
            var match = _regexContainsChildren.Match(line);
            if (match != Match.Empty)
            {
                hasChildren = true;
            }
            else
            {
                hasChildren = false;
                match = _regexContainsNoChildren.Match(line);
            }

            if (match == Match.Empty)
            {
                throw new System.Exception($"Did not a get a match for line {line}.");
            }

            return match;
        }

        private static string GetBagName(Match match)
        {
            return match.Groups[1].Value;
        }

        private static (string, int) GetChildBagInfo(Match match, int captureIndex)
        {
            int childBagCount = int.Parse(match.Groups[3].Captures[captureIndex].Value);
            string childBagName = match.Groups[4].Captures[captureIndex].Value;

            return (childBagName, childBagCount);
        }

        private void ProcessRelationships(Bag bag, Match match)
        {
            Debug.Assert(match.Groups[3].Captures.Count == match.Groups[4].Captures.Count, "counts of number matches and bag name matches should be identical for child bags");

            for (int i = 0; i < match.Groups[3].Captures.Count; i++)
            {
                (string childBagName, int childBagCount) = GetChildBagInfo(match, i);

                Bag childBag = _bagManager.GetOrInsertBagByName(childBagName, out bool _);

                bag.AddChild(childBag, childBagCount);
                childBag.AddParent(bag);
            }
        }
    }
}
