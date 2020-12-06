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
            catch (Exception e)
            {
                testsPass = false;
            }

            return testsPass;
        }
    }
}
