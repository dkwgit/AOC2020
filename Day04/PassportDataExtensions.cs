namespace AOC2020.Day04
{

    using System;
    using System.Text.RegularExpressions;

    static class PassportDataExtensions
    {
        public static bool ValidBirthYear(this PassportData data)
        {

            return ValidYear(data.BirthYear, 1920, 2002);
        }

        public static bool ValidIssueYear(this PassportData data)
        {
            return ValidYear(data.IssueYear, 2010, 2020);
        }

        public static bool ValidExpirationYear(this PassportData data)
        {
            return ValidYear(data.ExpirationYear, 2020, 2030);
        }

        public static bool ValidHeight(this PassportData data)
        {
            if (data.Height == string.Empty)
            {
                return false;
            }
            var match = new Regex(@"^(\d{2,3})(cm|in)$").Match(data.Height);
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

        public static bool ValidHairColor(this PassportData data)
        {
            if (data.HairColor == string.Empty)
            {
                return false;
            }
            bool match = new Regex(@"^#[0-9a-f]{6}$").IsMatch(data.HairColor);
            return match;
        }

        public static bool ValidEyeColor(this PassportData data)
        {
            if (data.EyeColor == string.Empty)
            {
                return false;
            }
            return new Regex(@"^(amb|blu|brn|gry|grn|hzl|oth)$").IsMatch(data.EyeColor);
        }

        public static bool ValidPassportId(this PassportData data)
        {
            if (data.PassportId == string.Empty)
            {
                return false;
            }
            return new Regex(@"^\d{9}$").IsMatch(data.PassportId);
        }

        private static bool ValidYear(String year, int min, int max)
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
