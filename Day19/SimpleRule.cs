namespace AOC2020.Day19
{
    using System;

    internal record SimpleRule : IAbstractRule
    {
        public bool IsVariableLength
        {
            get
            {
                return Value.IsVariableLength;
            }
        }

        public int ExpansionIncrement
        {
            get
            {
                return Value.ExpansionIncrement;
            }
        }

        public IAbstractRule Parent { get; init; }

        public int Id { get; init; }

        public string GeneratingExpression { get; init; }

        public IAbstractRule Value { get; init; }

        public int MatchLength { get; set; }

        public SimpleRule(IAbstractRule parent, int id, string generatingExpression, IAbstractRule value) => (Parent, Id, GeneratingExpression, Value) = (parent, id, generatingExpression, value);

        public bool Valid(ReadOnlySpan<char> expression)
        {
            return Value.Valid(expression);
        }

        public static SimpleRule Create(Puzzle p, IAbstractRule parent, int id, string expression)
        {
            int otherRule = int.Parse(expression);

            IAbstractRule rule = p.LoadRule(parent, otherRule);

            SimpleRule newRule = new SimpleRule(parent, id, expression, rule)
            {
                MatchLength = rule.MatchLength,
            };

            return newRule;
        }
    }
}
