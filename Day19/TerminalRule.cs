namespace AOC2020.Day19
{
    using System;
    using System.Diagnostics;

    internal record TerminalRule : IAbstractRule
    {
        public bool IsVariableLength { get; } = false;

        public int ExpansionIncrement { get; } = 0;

        public IAbstractRule Parent { get; init; }

        public int Id { get; init; }

        public ReadOnlyMemory<char> GeneratingExpression { get; init; }

        public ReadOnlyMemory<char> Value { get; init; }

        public int MatchLength { get; set; }

        public TerminalRule(IAbstractRule parent, int id, ReadOnlyMemory<char> generatingExpression, ReadOnlyMemory<char> value) => (Parent, Id, GeneratingExpression, Value, MatchLength) = (parent, id, generatingExpression, value, 1);

        public bool Valid(ReadOnlySpan<char> expression)
        {
            return expression.Equals(Value.Span, StringComparison.Ordinal);
        }

        public static TerminalRule Create(Puzzle _, IAbstractRule parent, int id, ReadOnlyMemory<char> expression)
        {
            return new TerminalRule(parent, id, expression, expression.Slice(1, 1));
        }
    }
}
