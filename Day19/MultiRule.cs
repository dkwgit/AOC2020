namespace AOC2020.Day19
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    record MultiRule : IAbstractRule
    {
        public IAbstractRule Parent { get; init; }

        public int Id { get; init; }

        public string GeneratingExpression { get; init; }

        public List<IAbstractRule> Value { get; init; }

        public int MatchLength { get; set; }

        public MultiRule(IAbstractRule parent, int id, string generatingExpression, List<IAbstractRule> value) => (Parent, Id, GeneratingExpression, Value) = (parent, id, generatingExpression, value);

        public bool Valid(string expression)
        {
            var ruleExpressionStringPairs = Value.Select((x, index) => (Rule: x, ExpressionString: expression.Substring(index == 0 ? index : Value[index - 1].MatchLength, Value[index].MatchLength))).ToList();
            foreach (var (rule, expressionString) in ruleExpressionStringPairs)
            {
                if (!rule.Valid(expressionString))
                {
                    return false;
                }
            }

            return true;
        }

        public static MultiRule Create(Puzzle p, IAbstractRule parent, int id, string expression)
        {
            var ruleIdList = expression.Split(' ').Select(x => int.Parse(x)).ToList();

            List<IAbstractRule> rules = new ();
            foreach (var ruleId in ruleIdList)
            {
                if (ruleId == id)
                {
                    rules.Add(new SelfRule(parent, id, expression));
                }
                else
                {
                    rules.Add(p.LoadRule(parent, ruleId));
                }
            }

            MultiRule rule = new MultiRule(parent, id, expression, rules)
            {
                MatchLength = rules.Sum(x => x.MatchLength),
            };

            return rule;
        }
    }
}
