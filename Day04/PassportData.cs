namespace AOC2020.Day04
{
    using System.Text.RegularExpressions;

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

        public bool HasAllRequiredFields()
        {
            return BirthYear != string.Empty &&
                IssueYear != string.Empty &&
                ExpirationYear != string.Empty &&
                Height != string.Empty &&
                HairColor != string.Empty &&
                EyeColor != string.Empty &&
                PassportId != string.Empty;
        }

        public bool ValidateFields()
        {
            return ValidBirthYear() &&
                ValidIssueYear() &&
                ValidExpirationYear() &&
                ValidHeight() &&
                ValidHairColor() &&
                ValidEyeColor() &&
                ValidPassportId();
        }

        public bool ValidBirthYear()
        {
            return ValidYear(BirthYear, 1920, 2002);
        }

        public bool ValidIssueYear()
        {
            return ValidYear(IssueYear, 2010, 2020);
        }

        public bool ValidExpirationYear()
        {
            return ValidYear(ExpirationYear, 2020, 2030);
        }

        public bool ValidHeight()
        {
            var match = new Regex(@"^(\d{2,3})(cm|in)$").Match(Height);

            if (!match.Success)
            {
                return false;
            }

            int height = int.Parse(match.Groups[1].Value);
            string suffix = match.Groups[2].Value;

            if (suffix == "cm" && 150 <= height && height <= 193)
            {
                return true;
            }

            if (suffix == "in" && 59 <= height && height <= 76)
            {
                return true;
            }

            return false;
        }

        public bool ValidHairColor()
        {
            bool match = new Regex(@"^#[0-9a-f]{6}$").IsMatch(HairColor);

            return match;
        }

        public bool ValidEyeColor()
        {
            return new Regex(@"^(amb|blu|brn|gry|grn|hzl|oth)$").IsMatch(EyeColor);
        }

        public bool ValidPassportId()
        {
            return new Regex(@"^\d{9}$").IsMatch(PassportId);
        }

        private static bool ValidYear(string year, int min, int max)
        {
            if (year == string.Empty)
            {
                return false;
            }

            if (!new Regex(@"^\d{4}$").IsMatch(year))
            {
                return false;
            }

            int yearNumber = int.Parse(year);

            return (min <= yearNumber) && (yearNumber <= max);
        }
    }
}
