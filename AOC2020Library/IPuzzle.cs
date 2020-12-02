using System;
using System.Collections.Generic;

namespace AOC2020Library
{
    public interface IPuzzle
    {
        void SetInput(List<string> input);
        List<String> Input { get; }
        string Part1 { get; }
        string Part2 { get; }
    }
}
