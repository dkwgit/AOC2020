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

        public int MatchLength { get; set; }

        public DoubleRule(IAbstractRule parent, int id, string generatingExpression, (IAbstractRule, IAbstractRule) value) => (Parent, Id, GeneratingExpression, Value) = (parent, id, generatingExpression, value);

        public bool Valid(string expression)
        {
            var (firstString, secondString) = (expression.Substring(0, Value.RuleOne.MatchLength), expression.Substring(Value.RuleOne.MatchLength, Value.RuleTwo.MatchLength));

            return Value.RuleOne.Valid(firstString) && Value.RuleTwo.Valid(secondString);
        }

        public static DoubleRule Create(Puzzle p, IAbstractRule parent, int id, string expression)
        {
            int firstRuleId = int.Parse(expression.Split(' ')[0]);
            int secondRuleId = int.Parse(expression.Split(' ')[1]);

            IAbstractRule firstRule = p.LoadRule(parent, firstRuleId);
            IAbstractRule secondRule = p.LoadRule(parent, secondRuleId);

            DoubleRule rule = new DoubleRule(parent, id, expression, (firstRule, secondRule))
            {
                MatchLength = firstRule.MatchLength + secondRule.MatchLength,
            };

            return rule;
        }
    }
}
