namespace AOC2020.Day19
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    record DoubleRule : IAbstractRule
    {
        public IAbstractRule Parent { get; init; }

        public int Id { get; init; }

        public string GeneratingExpression { get; init; }

        public (IAbstractRule RuleOne, IAbstractRule RuleTwo) Value { get; init; }

        public List<int> MatchLengths { get; set; }

        public DoubleRule(IAbstractRule parent, int id, string generatingExpression, (IAbstractRule, IAbstractRule) value) => (Parent, Id, GeneratingExpression, Value) = (parent, id, generatingExpression, value);

        public bool Valid(string expression)
        {
            var result = MatchLengths.Where(x => x == expression.Length).ToList();
            if (result.Count == 0)
            {
                return false;
            }

            Debug.Assert(result.Count == 1, $"Expecting exactly one result, but got {result.Count}");

            var pairs = (from first in Value.RuleOne.MatchLengths
                         from second in Value.RuleTwo.MatchLengths
                         where first + second == result[0]
                         select (FirstString: expression.Substring(0, first), SecondString: expression.Substring(first, second))).ToList();

            return pairs.Any(x => Value.RuleOne.Valid(x.FirstString) && Value.RuleTwo.Valid(x.SecondString));
        }

        public static DoubleRule Create(Puzzle p, IAbstractRule parent, int id, string expression)
        {
            int firstRuleId = int.Parse(expression.Split(' ')[0]);
            int secondRuleId = int.Parse(expression.Split(' ')[1]);

            IAbstractRule firstRule = p.LoadRule(parent, firstRuleId);
            IAbstractRule secondRule = p.LoadRule(parent, secondRuleId);

            List<int> combinedLengths = (from first in firstRule.MatchLengths
                                         from second in secondRule.MatchLengths
                                         select first + second).Distinct().ToList();

            DoubleRule rule = new DoubleRule(parent, id, expression, (firstRule, secondRule))
            {
                MatchLengths = combinedLengths,
            };
            return rule;
        }
    }
}
