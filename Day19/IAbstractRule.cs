namespace AOC2020.Day19
{
    using System.Collections.Generic;

    internal interface IAbstractRule
    {
        bool IsVariableLength { get; }

        int ExpansionIncrement { get; }

        IAbstractRule Parent { get; init; }

        int MatchLength { get; set; }

        int Id { get; init; }

        string GeneratingExpression { get; init; }

        bool Valid(string expression);
    }
}
