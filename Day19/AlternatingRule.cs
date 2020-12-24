namespace AOC2020.Day19
{
    using System.Diagnostics;
    using System.Linq;

    record AlternatingRule : IAbstractRule
    {
        public bool IsVariableLength
        {
            get
            {
                return Value.Or.GetType() == typeof(MultiRule) && (Value.Or as MultiRule).Value.Any(x => x.GetType() == typeof(SelfRule));
            }
        }

        public int ExpansionIncrement
        {
            get
            {
                if (!IsVariableLength)
                {
                    return 0;
                }

                return Value.Or.ExpansionIncrement;
            }
        }

        public IAbstractRule Parent { get; init; }

        public int Id { get; init; }

        public string GeneratingExpression { get; init; }

        public (IAbstractRule Either, IAbstractRule Or) Value { get; init; }

        public int MatchLength { get; set; }

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

            Debug.Assert(first.MatchLength == second.MatchLength, "Expecting both alternating rule items to have the same MatchLength");

            AlternatingRule rule = new AlternatingRule(parent, id, expression, (first, second))
            {
                MatchLength = first.MatchLength,
            };

            return rule;
        }

        public static IAbstractRule ResolveSubexpression(Puzzle p, IAbstractRule parent, int id, string expression)
        {
            IAbstractRule rule;

            if (expression.Contains(' '))
            {
                rule = MultiRule.Create(p, parent, id, expression);
            }
            else
            {
                rule = p.LoadRule(parent, int.Parse(expression));
            }

            return rule;
        }
    }
}
