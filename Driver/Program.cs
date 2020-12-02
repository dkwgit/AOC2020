using System;
using System.Collections.Generic;
using AOC2020Library;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            RunRegressionTests();
        }

        static void RunRegressionTests()
        {
            List<IPuzzle> list = new List<IPuzzle>();
            list.Add(new Day01.Puzzle());
            list.Add(new Day02.Puzzle());

            RegressionTests.RunRegressionTests(list);
        }
    }
}
