using System;
using System.Collections.Generic;

namespace AOC2020.Utilities
{
    public interface IPuzzle
    {
        void SetInput(List<string> input);
        List<String> Input { get; }
        string Part1 { get; }
        string Part2 { get; }
        string Day { get; }
    }
}
