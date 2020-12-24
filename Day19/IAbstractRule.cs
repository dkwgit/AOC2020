namespace AOC2020.Day19
{
    using System.Collections.Generic;

    internal interface IAbstractRule
    {
        IAbstractRule Parent { get; init; }

        List<int> MatchLengths { get; set; }

        int Id { get; init; }

        string GeneratingExpression { get; init; }

        bool Valid(string expression);
    }
}
