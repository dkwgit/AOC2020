namespace AOC2020.Driver
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        public static void Main()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            using var serviceProvider = serviceCollection.BuildServiceProvider();
            Driver driver = serviceProvider.GetService<AOC2020.Driver.Driver>();

            driver.Run();
        }

        public static void ConfigureServices(IServiceCollection collection)
        {
            collection
            .AddLogging(builder =>
             {
                 builder
                     .ClearProviders()
                     .SetMinimumLevel(LogLevel.Information)
                     .AddFilter("Microsoft", LogLevel.Warning)
                     .AddFilter("System", LogLevel.Warning)
                     .AddSimpleConsole(options =>
                         {
                             options.IncludeScopes = true;
                             options.SingleLine = true;
                             options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff ";
                         })
                     .AddDebug();
             })
            .AddSingleton<Driver>()
            .AddSingleton<AOC2020.Utilities.RegressionTestingDriver>()
            .AddTransient<AOC2020.Day01.Puzzle>()
            .AddTransient<AOC2020.Day02.Puzzle>()
            .AddTransient<AOC2020.Day03.Puzzle>()
            .AddTransient<AOC2020.Day04.Puzzle>()
            .AddTransient<AOC2020.Day05.Puzzle>()
            .AddTransient<AOC2020.Day06.Puzzle>()
            .AddTransient<AOC2020.Day07.Puzzle>()
            .AddTransient<AOC2020.Day08.Puzzle>()
            .AddTransient<AOC2020.Day09.Puzzle>()
            .AddTransient<AOC2020.Day10.Puzzle>()
            .AddTransient<AOC2020.Day11.Puzzle>()
            .AddTransient<AOC2020.Day12.Puzzle>()
            .AddTransient<AOC2020.Day13.Puzzle>()
            .AddTransient<AOC2020.Day14.Puzzle>()
            .AddTransient<AOC2020.Day15.Puzzle>()
            .AddTransient<AOC2020.Day16.Puzzle>()
            .AddTransient<AOC2020.Day17.Puzzle>()
            .AddTransient<AOC2020.Day18.Puzzle>()
            .AddTransient<AOC2020.Day19.Puzzle>()
            .AddTransient<AOC2020.Day20.Puzzle>()
            .AddTransient<AOC2020.Day21.Puzzle>()
            .AddTransient<AOC2020.Day22.Puzzle>();
        }
    }
}
