using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020.Day02
{
    record PasswordEntry
    {
        public int Min { get; init; }
        public int Max { get; init; }
        public char Letter { get; init; }
        public string Password { get; init; }

        public PasswordEntry(int min, int max, char letter, string password) => (Min, Max, Letter, Password) = (min, max, letter, password);
    }
}
