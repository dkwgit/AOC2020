using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using AOC2020Library;

namespace Day02
{
    public class Puzzle : IPuzzle
    {
        class PasswordEntry
        {
            private int _min = -1;
            private int _max = -1;
            private char _letter = '\0';
            private string _password = null;

            public int Min
            {
                get
                {
                    return _min;
                }
            }
            public int Max
            {
                get
                {
                    return _max;
                }
            }

            public char Letter
            {
                get
                {
                    return _letter;
                }
            }

            public string Password
            {
                get
                {
                    return _password;
                }
            }

            public PasswordEntry(int min, int max, char letter, string password)
            {
                _min = min;
                _max = max;
                _letter = letter;
                _password = password;
            }
        }
        private List<string> _input = null;
        private List<PasswordEntry> _passwords = new List<PasswordEntry>();

        private bool Part1Predicate(PasswordEntry entry)
        {
            int countInPassword = entry.Password.Count(x => x == entry.Letter);
            if (entry.Min <= countInPassword && countInPassword <= entry.Max)
            {
                return true;
            }
            return false;
        }

        private bool Part2Predicate(PasswordEntry entry)
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

        public string Day => "02";

        public List<String> Input
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
                string answer = $"{_passwords.Where(x => Part1Predicate(x)).Count()}";
                Console.WriteLine($"Found {answer} passwords that were valid out of {_passwords.Count()}");
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = $"{_passwords.Where(x => Part2Predicate(x)).Count()}";
                Console.WriteLine($"Found {answer} passwords that were valid out of {_passwords.Count()}");
                return answer;
            }
        }

        public void SetInput(List<string> input)
        {
            _input = input;
            foreach(var p in input)
            {
                string pattern = @"^(\d+)-(\d+)\s+(.):\s+(.*)$";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(p);
                if (match.Success)
                {
                    int min = int.Parse(match.Groups[1].Value);
                    int max = int.Parse(match.Groups[2].Value);
                    char letter = char.Parse(match.Groups[3].Value);
                    string password = match.Groups[4].Value;
                    PasswordEntry entry = new PasswordEntry(min, max, letter, password);
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
