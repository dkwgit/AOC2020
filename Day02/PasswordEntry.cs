namespace AOC2020.Day02
{
    record PasswordEntry
    {
        public int Min { get; init; }
        public int Max { get; init; }
        public char Letter { get; init; }
        public string Password { get; init; }
    }
}
