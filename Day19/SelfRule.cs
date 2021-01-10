namespace AOC2020.Day19
{
    using System;

    internal record SelfRule : IAbstractRule
    {
        public bool IsVariableLength { get; } = false;

        public int ExpansionIncrement { get; } = 0;

        public IAbstractRule Parent { get; init; }

        public int Id { get; init; }

        public ReadOnlyMemory<char> GeneratingExpression { get; init; }

        public IAbstractRule Value { get; init; }

        public int MatchLength { get; set; }

        public SelfRule(IAbstractRule parent, int id, ReadOnlyMemory<char> generatingExpression) => (Parent, Id, GeneratingExpression, Value, MatchLength) = (parent, id, generatingExpression, parent, 0);

        public bool Valid(ReadOnlySpan<char> expression)
        {
            return true;
        }
    }
}