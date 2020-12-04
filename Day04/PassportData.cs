namespace AOC2020.Day04
{
    record PassportData
    {
        public string BirthYear { get; init; }

        public string IssueYear { get; init; }

        public string ExpirationYear { get; init; }

        public string Height { get; init; }

        public string HairColor { get; init; }

        public string EyeColor { get; init; }

        public string PassportId { get; init; }

        public string CountryId { get; init; }
    }
}
