namespace AOC2020.Utilities
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;

    public class RegressionTestingDriver
    {
        private readonly ILogger _logger;

        private readonly IServiceProvider _serviceProvider;

        public RegressionTestingDriver(IServiceProvider serviceProvider, ILogger<RegressionTestingDriver> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public bool RunPuzzleRegressionTests()
        {
            bool testsPass = true;
            DataFixture fixture = new ();

            try
            {
                foreach (var puzzleData in fixture.GetPuzzleData())
                {
                    Type t = Type.GetType($"AOC2020.Day{puzzleData.Day}.Puzzle, Day{puzzleData.Day}");
                    var puzzle = (IPuzzle)_serviceProvider.GetService(t) as IPuzzle;

                    puzzle.RegressionTest(puzzleData);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception encountered during regression test");
                testsPass = false;
            }

            return testsPass;
        }
    }
}
