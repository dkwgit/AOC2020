namespace AOC2020.Day13
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using AOC2020.Utilities;
    using ExtendedArithmetic;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        private List<int> _buses = new ();

        private List<(int Bus, int Index)> _busesAndIndices = new ();

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
                var (desiredTime, bus, arrivalTime, waitTime, index) = _buses.Select((x, index) => CalculateBusInfoForTS(_targetTimestamp, x, index)).OrderBy(x => x.WaitTime).First();
                string answer = (waitTime * bus).ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as the multiplied result of bus number {bus} and lowest wait time {waitTime} at time stamp {timestamp}", Day, answer, bus, waitTime, _targetTimestamp);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                // retrieve a list of triplets (Bus, RemainderAt0, Index), where remainderAt0 is the remainder needed at timestamp index 0
                var info = _busesAndIndices.Select(x => (x.Bus, RemainderAt0: GetRemainderAt0ForCRT(x.Bus, x.Index), x.Index)).Where(x => x.Bus != -1).ToList();

                BigInteger[] buses = new BigInteger[info.Count];
                BigInteger[] remaindersAt0 = new BigInteger[info.Count];
                for (int i = 0; i < info.Count; i++)
                {
                    (int bus, int remainderAt0, int index) = info[i];
                    buses[i] = bus;
                    remaindersAt0[i] = remainderAt0;
                }

                var result = Polynomial.Algorithms.ChineseRemainderTheorem(buses, remaindersAt0);
                string answer = result.ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} as the time stamp at which the correct bus sequence begins", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
            _targetTimestamp = int.Parse(_input[0]);

            _buses = _input[1].Split(',').Where(s => s != "x").Select(x => int.Parse(x)).ToList();

            // retrieve as (bus, index) tuples, with -1 for buses denotex as 'x'
            _busesAndIndices = _input[1].Split(',').Select((x, i) => (Bus: (x != "x") ? int.Parse(x) : -1, Index: i)).ToList();
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

        private static int GetRemainderAt0ForCRT(int bus, int index)
        {
            // Calculate the remainder needed at index 0 (starting timestamp) to yield a remainder of 0 at the relevat index for a given bus
            int remainderAt0 = (bus - index) % bus;
            if (remainderAt0 < 0)
            {
                // adjust back into positive terrain
                remainderAt0 = bus + remainderAt0;
            }

            return remainderAt0;
        }
    }
}
