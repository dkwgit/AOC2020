namespace AOC2020.Day02
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
                string answer = $"{_passwords.Count(x => Part1Predicate(x))}";
                Console.WriteLine($"Found {answer} passwords that were valid out of {_passwords.Count}");
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = $"{_passwords.Where(x => Part2Predicate(x)).Count()}";
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

        private static bool Part1Predicate(PasswordEntry entry)
        {
            int countInPassword = entry.Password.Count(x => x == entry.Letter);

            if (entry.Min <= countInPassword && countInPassword <= entry.Max)
            {
                return true;
            }

            return false;
        }

        private static bool Part2Predicate(PasswordEntry entry)
        {
            int min = entry.Min;
            int max = entry.Max;
            int passwordLength = entry.Password.Length;
            char letter = entry.Letter;
            string password = entry.Password;

            if (min <= passwordLength && max <= passwordLength)
            {
                if (password[min - 1] == letter ^ password[max - 1] == letter)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
