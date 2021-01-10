namespace AOC2020.Day19
{
    using System;

    internal interface IAbstractRule
    {
        bool IsVariableLength { get; }

        int ExpansionIncrement { get; }

        IAbstractRule Parent { get; init; }

        int MatchLength { get; set; }

        int Id { get; init; }

        ReadOnlyMemory<char> GeneratingExpression { get; init; }

        bool Valid(ReadOnlySpan<char> expression);
    }
}
