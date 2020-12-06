namespace AOC2020.Driver
{
    using System;
    using System.Collections.Generic;
    using AOC2020.Utilities;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Driver
    {
        private readonly ILogger _logger;

        private readonly IServiceProvider _serviceProvider;

        public Driver(IServiceProvider serviceProvider, ILogger<Driver> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("Starting regression tests");
            RunPuzzleRegressionTests();
            _logger.LogInformation("Done with regression tests");
        }

        public void RunPuzzleRegressionTests()
        {
            RegressionTesting.RunPuzzleRegressionTests(
                new List<IPuzzle>
                {
                    _serviceProvider.GetService<AOC2020.Day01.Puzzle>(),
                    _serviceProvider.GetService<AOC2020.Day02.Puzzle>(),
                    _serviceProvider.GetService<AOC2020.Day03.Puzzle>(),
                    _serviceProvider.GetService<AOC2020.Day04.Puzzle>(),
                    _serviceProvider.GetService<AOC2020.Day05.Puzzle>(),
                    _serviceProvider.GetService<AOC2020.Day06.Puzzle>(),
                });
        }
    }
}
