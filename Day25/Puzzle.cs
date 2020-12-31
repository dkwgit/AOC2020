namespace AOC2020.Day25
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "25";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                long doorPublic = long.Parse(_input[0]);
                long cardPublic = long.Parse(_input[1]);
                long doorLoopValue = FindLoopvalue(doorPublic);
                long cardLoopValue = FindLoopvalue(cardPublic);

                long doorEncryptionKey = Transform(doorPublic, cardLoopValue);
                long cardEncryptionKey = Transform(cardPublic, doorLoopValue);
                Debug.Assert(doorEncryptionKey == cardEncryptionKey, "Both transforms should produce the same encryption key");

                string answer = doorEncryptionKey.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as encryption key after finding loop sizes that generated public keys", Day, answer);
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
        }

        private static long Transform(long subject, long loopValue)
        {
            long divisor = 20201227;
            long value = 1;

            for (long i = 0; i < loopValue; i++)
            {
                value = value * subject;
                value = value % divisor;
            }

            return value;
        }

        private static long FindLoopvalue(long valueToGenerate)
        {
            long subject = 7;
            long value = 1;
            long loop = 0;
            long divisor = 20201227;
            bool looking = true;

            while (looking)
            {
                value = value * subject;
                value = value % divisor;
                if (value == valueToGenerate)
                {
                    break;
                }

                loop++;
            }

            return loop + 1;
        }
    }
}
