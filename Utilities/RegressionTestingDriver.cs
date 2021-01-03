namespace AOC2020.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
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

        public bool RunPuzzleRegressionTests(out List<(string day, string label, long timing)> timings)
        {
            bool testsPass = true;
            DataFixture fixture = new ();
            timings = new ();

            try
            {
                foreach (var puzzleData in fixture.GetPuzzleData())
                {
                    Type t = Type.GetType($"AOC2020.Day{puzzleData.Day}.Puzzle, Day{puzzleData.Day}");
                    var puzzle = (IPuzzle)_serviceProvider.GetService(t) as IPuzzle;

                    _logger.LogInformation("Starting test of type {type} for Day {day}", puzzleData.Type, puzzleData.Day);

                    Stopwatch s = Stopwatch.StartNew();
                    puzzle.RegressionTest(puzzleData);
                    s.Stop();
                    timings.Add((puzzleData.Day, puzzleData.Type, s.ElapsedMilliseconds));

                    _logger.LogInformation("Finished");
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
