namespace AOC2020.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AOC2020.Day14Computer;
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

        public string Day => "14";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                // mem[17610] = 1035852 is an example of what this patter handles
                string pattern = @"^mem\[(\d+)\]\s=\s(\d+)$";
                Regex regex = new Regex(pattern);

                Day14Computer<ValueMaskingStrategy> computer = new Day14Computer<ValueMaskingStrategy>(36);
                foreach (var instruction in Input)
                {
                    if (instruction.Contains("mask"))
                    {
                        var mask = instruction[7..];
                        computer.MaskString = mask;
                    }
                    else
                    {
                        Match match = regex.Match(instruction);
                        if (match.Success)
                        {
                            int location = int.Parse(match.Groups[1].Value);
                            long value = long.Parse(match.Groups[2].Value);
                            computer.WriteValue(location, value);
                        }
                        else
                        {
                            throw new InvalidOperationException($"instruction {instruction} not handled by Regex pattern {pattern}");
                        }
                    }
                }

                var result = computer.GetAllMemoryValues().Sum();
                string answer = result.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as the sum of all numbers in the computers memory", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string pattern = @"^mem\[(\d+)\]\s=\s(\d+)$";
                Regex regex = new Regex(pattern);

                Day14Computer<LocationMaskingStrategy> computer = new Day14Computer<LocationMaskingStrategy>(36);

                foreach (var instruction in Input)
                {
                    if (instruction.Contains("mask"))
                    {
                        var mask = instruction[7..];
                        computer.MaskString = mask;
                    }
                    else
                    {
                        Match match = regex.Match(instruction);
                        if (match.Success)
                        {
                            int location = int.Parse(match.Groups[1].Value);
                            long value = long.Parse(match.Groups[2].Value);
                            computer.WriteValue(location, value);
                        }
                        else
                        {
                            throw new InvalidOperationException($"instruction {instruction} not handled by Regex pattern {pattern}");
                        }
                    }
                }

                var result = computer.GetAllMemoryValues().Sum();
                string answer = result.ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} as the sum of all numbers in the computers memory", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
        }
    }
}
