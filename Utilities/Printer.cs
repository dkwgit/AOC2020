namespace AOC2020.Utilities
{
    using System;
    using System.Collections.Generic;

    public record Printer
    {
        private readonly List<string> _input;

        public Printer(List<string> input)
        {
            _input = input;
        }

        public void Print()
        {
            Console.WriteLine("Output:");
            foreach (var line in _input)
            {
                Console.WriteLine($"\t{line}");
            }
        }
    }
}
