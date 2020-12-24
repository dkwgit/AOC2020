namespace AOC2020.Day19
{
    using System.Collections.Generic;

    internal record SimpleRule : IAbstractRule
    {
        public IAbstractRule Parent { get; init; }

        public int Id { get; init; }

        public string GeneratingExpression { get; init; }

        public IAbstractRule Value { get; init; }

        public List<int> MatchLengths { get; set; }

        public SimpleRule(IAbstractRule parent, int id, string generatingExpression, IAbstractRule value) => (Parent, Id, GeneratingExpression, Value) = (parent, id, generatingExpression, value);

        public bool Valid(string expression)
        {
            return Value.Valid(expression);
        }

        public static SimpleRule Create(Puzzle p, IAbstractRule parent, int id, string expression)
        {
            int otherRule = int.Parse(expression);

            IAbstractRule rule = p.LoadRule(parent, otherRule);

            SimpleRule newRule = new SimpleRule(parent, id, expression, rule)
            {
                MatchLengths = rule.MatchLengths,
            };

            return newRule;
        }
    }
}
