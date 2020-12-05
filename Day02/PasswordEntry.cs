namespace AOC2020.Day02
{
    using System.Linq;

    record PasswordEntry
    {
        public int Min { get; init; }
        public int Max { get; init; }
        public char Letter { get; init; }
        public string Password { get; init; }

        // This is validity by standards of Part1 of puzzle
        public bool ValidByLetterCount()
        {
            int countInPassword = Password.Count(x => x == Letter);

            if (Min <= countInPassword && countInPassword <= Max)
            {
                return true;
            }

            return false;
        }

        // This is validity by standards of Part2 of puzzle
        public bool ValidByLetterPosition()
        {
            if (Min <= Password.Length && Max <= Password.Length)
            {
                if (Password[Min - 1] == Letter ^ Password[Max - 1] == Letter)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
