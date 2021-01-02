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

        public bool RunPuzzleRegressionTests()
        {
            bool testsPass = true;
            DataFixture fixture = new ();

            try
            {
                List<(string day, string label, long timing)> timings = new ();
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

                _logger.LogInformation("All tests cumulative time in milliseconds {cumulative}", timings.Sum(x => x.timing));
                _logger.LogInformation("Logging timing information for individual tests");
                foreach (var item in timings.OrderByDescending(x => x.timing))
                {
                    _logger.LogInformation("Test {day}, with label {label} took {time} milliseconds", item.day, item.label, item.timing);
                }

                _logger.LogInformation("Finished logging timing");
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
