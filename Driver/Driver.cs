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

        public Driver(IServiceProvider serviceProvider, ILogger<Driver> logger) => (_serviceProvider, _logger) = (serviceProvider, logger);

        public void Run()
        {
            _logger.LogInformation("Starting regression tests");
            RunPuzzleRegressionTests();
            _logger.LogInformation("Done with regression tests");
        }

        public void RunPuzzleRegressionTests()
        {
            RegressionTestingDriver testingDriver = _serviceProvider.GetService<AOC2020.Utilities.RegressionTestingDriver>();

            testingDriver.RunPuzzleRegressionTests();
        }
    }
}
