﻿namespace AOC2020.Day02
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AOC2020.Utilities;

    public class Puzzle : IPuzzle
    {
        private readonly List<PasswordEntry> _passwords = new ();

        private List<string> _input = null;

        public string Day => "02";

        public List<string> Input
        {
            get
            {
                return _input;
            }
        }

        public string Part1
        {
            get
            {
                string answer = $"{_passwords.Count(x => x.ValidByLetterCount())}";
                Console.WriteLine($"Found {answer} passwords that were valid out of {_passwords.Count}");
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = $"{_passwords.Where(x => x.ValidByLetterPosition()).Count()}";
                Console.WriteLine($"Found {answer} passwords that were valid out of {_passwords.Count}");
                return answer;
            }
        }

        public void ProcessPuzzleInput()
        {
            _input = new PuzzleDataStore().GetPuzzleInputAsList(Day, false);
            foreach (var p in _input)
            {
                string pattern = @"^(\d+)-(\d+)\s+(.):\s+(.*)$";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(p);
                if (match.Success)
                {
                    PasswordEntry entry = new ()
                    {
                        Min = int.Parse(match.Groups[1].Value),
                        Max = int.Parse(match.Groups[2].Value),
                        Letter = char.Parse(match.Groups[3].Value),
                        Password = match.Groups[4].Value,
                    };
                    _passwords.Add(entry);
                }
                else
                {
                    throw new Exception($"Regex match failure on input {p}");
                }
            }
        }
    }
}
