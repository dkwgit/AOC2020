namespace AOC2020.Day08
{
    using System.Collections.Generic;
    using AOC2020.Computer;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private Processor _processor;

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "08";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                string answer = _processor.Run(out bool looped).ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as accumulator value just before program would start to loop", Day, answer);

                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = _processor.Fix().ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} as accumulator value when the program is patched to not loop", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;

            Program p = new Program();
            foreach (var line in _input)
            {
                p.AddInstruction(line);
            }

            _processor = new Processor(p);
        }
    }
}
