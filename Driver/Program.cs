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
            .AddSingleton<AOC2020.Day01.Puzzle>()
            .AddSingleton<AOC2020.Day02.Puzzle>()
            .AddSingleton<AOC2020.Day03.Puzzle>()
            .AddSingleton<AOC2020.Day04.Puzzle>()
            .AddSingleton<AOC2020.Day05.Puzzle>();
        }
    }
}
