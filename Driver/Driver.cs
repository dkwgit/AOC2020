namespace AOC2020.Driver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Driver
    {
        private readonly ILogger _logger;

        private readonly IServiceProvider _serviceProvider;

        public Driver(IServiceProvider serviceProvider, ILogger<Driver> logger) => (_serviceProvider, _logger) = (serviceProvider, logger);

        public void Run(int numberOfRuns)
        {
            List<List<(string day, string label, long timing)>> timingsForEachRun = new ();

            for (int i = 0; i < numberOfRuns; i++)
            {
                _logger.LogInformation("Starting regression tests, run #{run}", i + 1);
                RunPuzzleRegressionTests(out List<(string day, string label, long timing)> timings);
                timingsForEachRun.Add(timings);
                long total = timings.Sum(x => x.timing);
                _logger.LogInformation("Done with regression tests, run #{run}", i + 1);
            }

            _logger.LogInformation("Logging timing information");
            int timingCount = 0;
            foreach (var timing in timingsForEachRun)
            {
                long total = timing.Sum(x => x.timing);
                _logger.LogInformation("Timings for run #{run}", timingCount + 1);
                _logger.LogInformation("Cumulative time in milliseconds {cumulative}, with an average of {Avg}", total, total / timing.Count);
                _logger.LogInformation("Logging timing information for individual tests");

                foreach (var item in timing.OrderByDescending(x => x.timing))
                {
                    _logger.LogInformation("Test {day}, with label {label} took {time} milliseconds", item.day, item.label, item.timing);
                }

                timingCount++;
            }

            long totalForAllRuns = timingsForEachRun.SelectMany(x => x.Select(x => x.timing)).Sum(x => x);
            _logger.LogInformation("Cumulative time in milliseconds for total count of runs {numberOfRuns}: {cumulative}, with an average of {Avg}", numberOfRuns, totalForAllRuns, totalForAllRuns / numberOfRuns);
            _logger.LogInformation("Finished logging timing");
        }

        public void RunPuzzleRegressionTests(out List<(string day, string label, long timing)> timingInfo)
        {
            RegressionTestingDriver testingDriver = _serviceProvider.GetService<AOC2020.Utilities.RegressionTestingDriver>();
            testingDriver.RunPuzzleRegressionTests(out List<(string day, string label, long timing)> timings);

            timingInfo = timings;
        }
    }
}
