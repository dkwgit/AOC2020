namespace AOC2020.Utilities
{
    using System;
    using System.Collections.Generic;

    public static class RegressionTesting
    {
        public static bool RunPuzzleRegressionTests(List<IPuzzle> puzzles)
        {
            bool testsPass = true;
            try
            {
                foreach (var puzzle in puzzles)
                {
                    puzzle.RegressionTest();
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            catch (Exception e)
#pragma warning restore IDE0059 // Unnecessary assignment of a value
#pragma warning restore CS0168 // Variable is declared but never used
            {
                testsPass = false;
            }

            return testsPass;
        }
    }
}
