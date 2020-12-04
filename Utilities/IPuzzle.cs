namespace AOC2020.Utilities
{
    using System;
    using System.Collections.Generic;

    public interface IPuzzle
    {
        List<string> Input { get; }

        string Part1 { get; }

        string Part2 { get; }

        string Day { get; }

        void ProcessPuzzleInput();
    }
}
