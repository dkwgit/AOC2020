﻿namespace AOC2020.Driver
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
            _logger.LogInformation("Starting Driver.Run()");

            try
            {
                List<List<(string day, string label, long timing)>> timingsForEachRun = new ();

                for (int i = 0; i < numberOfRuns; i++)
                {
                    #region loggingRun
                    _logger.LogInformation("Run {run}: starting regression tests.", i + 1);
                    #endregion

                    RunPuzzleRegressionTests(out List<(string day, string label, long timing)> timings);
                    timingsForEachRun.Add(timings);

                    #region loggingRun
                    _logger.LogInformation("Run {run}: done with regression tests.", i + 1);
                    #endregion
                }

                #region loggingTimingForRuns
                _logger.LogInformation("Timing: Logging timing information for each run");
                int timingCount = 0;
                foreach (var timing in timingsForEachRun)
                {
                    long total = timing.Sum(x => x.timing);
                    _logger.LogInformation("Timing: Run {run} cumulative time for in milliseconds {cumulative} for running all days, with an average of {Avg} per day", timingCount + 1, total, total / timing.Count);
                    _logger.LogInformation("Timing: Run {run} Logging timing information for individual tests  day", timingCount + 1);

                    foreach (var item in timing.OrderByDescending(x => x.timing))
                    {
                        _logger.LogInformation("Timing Run {run} Test {day}, with label {label} took {time} milliseconds", timingCount + 1, item.day, item.label, item.timing);
                    }

                    timingCount++;
                }
                #endregion

                #region CumulativeTimingPerRunBlock
                long totalForAllRuns = timingsForEachRun.SelectMany(x => x.Select(x => x.timing)).Sum(x => x);
                _logger.LogInformation("Timing: Cumulative: Logging timing information averaging across runs");
                _logger.LogInformation("Timing: Cumulative: time in milliseconds for total count of runs {numberOfRuns}: {cumulative}, with an average of {Avg}", numberOfRuns, totalForAllRuns, totalForAllRuns / numberOfRuns);
                #endregion

                #region AverageTimingPerDayBlock
                _logger.LogInformation("Timing: Average by Day: Logging average timing information for each day across the runs, in descending order");
                (int day, long timing)[] dayTimings = new (int day, long timing)[timingsForEachRun[0].Count];
                for (int i = 0; i < numberOfRuns; i++)
                {
                    for (int d = 0; d < timingsForEachRun[i].Count; d++)
                    {
                        (int _, long timing) = dayTimings[d];
                        timing += timingsForEachRun[i][d].timing;
                        dayTimings[d] = (d + 1, timing);
                        string dayString = (d + 1).ToString();
                        if (dayString.Length == 1)
                        {
                            dayString = "0" + dayString;
                        }
                    }
                }

                foreach (var dayTiming in dayTimings.OrderByDescending(x => x.timing))
                {
                    string dayString = dayTiming.day.ToString();
                    if (dayString.Length == 1)
                    {
                        dayString = "0" + dayString;
                    }

                    _logger.LogInformation("Timing: Average by Day: for day {daystring} across the runs: {avgTiming}", dayString, dayTiming.timing / numberOfRuns);
                }
                #endregion
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception during Driver.Run()");
                throw;
            }

             #region finishLogging
            _logger.LogInformation("Finished logging timing");
            _logger.LogInformation("Finished Driver.Run()");
            #endregion
        }

        public void RunPuzzleRegressionTests(out List<(string day, string label, long timing)> timingInfo)
        {
            RegressionTestingDriver testingDriver = _serviceProvider.GetService<AOC2020.Utilities.RegressionTestingDriver>();
            testingDriver.RunPuzzleRegressionTests(out List<(string day, string label, long timing)> timings);

            timingInfo = timings;
        }
    }
}
