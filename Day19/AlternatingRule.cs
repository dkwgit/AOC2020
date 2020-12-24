namespace AOC2020.Day19
{
    using System.Collections.Generic;
    using System.Linq;

    record AlternatingRule : IAbstractRule
    {
        public IAbstractRule Parent { get; init; }

        public int Id { get; init; }

        public string GeneratingExpression { get; init; }

        public (IAbstractRule Either, IAbstractRule Or) Value { get; init; }

        public List<int> MatchLengths { get; set; }

        public AlternatingRule(IAbstractRule parent, int id, string generatingExpression, (IAbstractRule, IAbstractRule) value) => (Parent, Id, GeneratingExpression, Value) = (parent, id, generatingExpression, value);

        public bool Valid(string expression)
        {
            return Value.Either.Valid(expression) || Value.Or.Valid(expression);
        }

        public static AlternatingRule Create(Puzzle p, IAbstractRule parent, int id, string expression)
        {
            string expressionOne = expression.Split('|')[0].Trim();
            string expressionTwo = expression.Split('|')[1].Trim();

            IAbstractRule first = ResolveSubexpression(p, parent, id, expressionOne);
            IAbstractRule second = ResolveSubexpression(p, parent, id, expressionTwo);

            List<int> combinedLengths = new ();
            combinedLengths.AddRange(first.MatchLengths);
            combinedLengths.AddRange(second.MatchLengths);
            combinedLengths = combinedLengths.Distinct().ToList();

            AlternatingRule rule = new AlternatingRule(parent, id, expression, (first, second))
            {
                MatchLengths = combinedLengths,
            };

            return rule;
        }

        public static IAbstractRule ResolveSubexpression(Puzzle p, IAbstractRule parent, int id, string expression)
        {
            IAbstractRule rule;

            if (expression.Contains(' '))
            {
                rule = DoubleRule.Create(p, parent, id, expression);
            }
            else
            {
                rule = p.LoadRule(parent, int.Parse(expression));
            }

            return rule;
        }
    }
}
