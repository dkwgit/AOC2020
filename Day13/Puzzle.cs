namespace AOC2020.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        private List<int> _buses = new ();

        private int _targetTimestamp = -1;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "13";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                var result = _buses.Select((x, index) => CalculateBusInfoForTS(_targetTimestamp, x, index)).OrderBy(x => x.WaitTime).First();
                string answer = (result.WaitTime * result.Bus).ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as the multiplied result of bus number {bus} and lowest wait time {waitTime} at time stamp {timestamp}", Day, answer, result.Bus, result.WaitTime, _targetTimestamp);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = string.Empty;
                _logger.LogInformation("{Day}/Part2: Found {answer}", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
            _targetTimestamp = int.Parse(_input[0]);
            _buses = _input[1].Split(',').Where(s => s != "x").Select(x => int.Parse(x)).ToList();
        }

        private static (int DesiredTime, int Bus, int ArrivalTime, int WaitTime, int Index) CalculateBusInfoForTS(int timeStamp, int bus, int index)
        {
            int rem = timeStamp % bus;
            int div = timeStamp / bus;
            if (rem != 0)
            {
                div++;
            }

            return (DesiredTime: timeStamp, Bus: bus, ArrivalTime: bus * div, WaitTime: (bus * div) - timeStamp, Index: index);
        }
    }
}
