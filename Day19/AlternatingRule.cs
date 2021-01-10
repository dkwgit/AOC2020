namespace AOC2020.Day19
{
    using System;
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

        public ReadOnlyMemory<char> GeneratingExpression { get; init; }

        public (IAbstractRule Either, IAbstractRule Or) Value { get; init; }

        public int MatchLength { get; set; }

        public AlternatingRule(IAbstractRule parent, int id, ReadOnlyMemory<char> generatingExpression, (IAbstractRule, IAbstractRule) value)
        {
            Parent = parent;
            Id = id;
            GeneratingExpression = generatingExpression;
            Value = value;
        }

        public bool Valid(ReadOnlySpan<char> expression)
        {
            return Value.Either.Valid(expression) || Value.Or.Valid(expression);
        }

        public static AlternatingRule Create(Puzzle p, IAbstractRule parent, int id, ReadOnlyMemory<char> expression)
        {
            ReadOnlyMemory<char> expressionOne = null;
            ReadOnlyMemory<char> expressionTwo = null;
            ReadOnlySpan<char> span = expression.Span;
            for (int i = 0; i < expression.Length; i++)
            {
                if (span[i] == '|')
                {
                    expressionOne = expression.Slice(0, i - 1); // There's a space before the |, and we don't want to 'copy' that
                    expressionTwo = expression.Slice(i + 2); // There's a space after the |, and we want to point to first non-space after it
                }
            }

            IAbstractRule first = ResolveSubexpression(p, parent, id, expressionOne);
            IAbstractRule second = ResolveSubexpression(p, parent, id, expressionTwo);

            Debug.Assert(first.MatchLength == second.MatchLength, "Expecting both alternating rule items to have the same MatchLength");

            AlternatingRule rule = new AlternatingRule(parent, id, expression, (first, second))
            {
                MatchLength = first.MatchLength,
            };

            return rule;
        }

        public static IAbstractRule ResolveSubexpression(Puzzle p, IAbstractRule parent, int id, ReadOnlyMemory<char> expression)
        {
            IAbstractRule rule;

            if (expression.Span.Contains(' '))
            {
                rule = MultiRule.Create(p, parent, id, expression);
            }
            else
            {
                rule = p.LoadRule(parent, int.Parse(expression.Span));
            }

            return rule;
        }
    }
}
