namespace AOC2020.Day04
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AOC2020.Utilities;
    

    public class Puzzle : IPuzzle
    {
        private readonly List<PassportData> _passportData = new();

        private List<string> _input;
        
        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                string answer = _passportData.Count(x => Part1Predicate(x)).ToString();
                Console.WriteLine($"Found {answer} passports that were valid for part1 out of {_passportData.Count}");
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = _passportData.Count(x => Part2Predicate(x)).ToString();
                Console.WriteLine($"Found {answer} passports that were valid for part2 out of {_passportData.Count}");
                return answer;
            }
        }

        public string Day => "04";

        public void SetInput(List<string> input)
        {
            _input = input;
            List<string> dataItems = new();

            for (int index = 0; index < input.Count; index++)
            {
                string line = input[index].Trim();

                if (!string.IsNullOrEmpty(line))
                {
                    var array = line.Split(" ", System.StringSplitOptions.None);
                    dataItems.AddRange(array);
                }
                else
                {
                    _passportData.Add(GetPassportData(dataItems));
                    dataItems.Clear();
                }
            }
        }

        private static PassportData GetPassportData(List<string> dataItems)
        {
            string birthYear = string.Empty;
            string issueYear = string.Empty;
            string expirationYear = string.Empty;
            string height = string.Empty;
            string hairColor = string.Empty;
            string eyeColor = string.Empty;
            string passportId = string.Empty;
            string countryId = string.Empty;

            foreach (var item in dataItems)
            {
                var (prefix, suffix, _) = item.Split(":");

                switch (prefix)
                {
                    case "byr": birthYear = suffix;
                        break;
                    case "iyr": issueYear = suffix;
                        break;
                    case "eyr": expirationYear = suffix;
                        break;
                    case "hgt": height = suffix;
                        break;
                    case "hcl": hairColor = suffix;
                        break;
                    case "ecl": eyeColor = suffix;
                        break;
                    case "pid": passportId = suffix;
                        break;
                    case "cid": countryId = suffix;
                        break;
                    default:
                        throw new System.Exception($"Bad value {prefix} in switch statement");
#pragma warning disable CS0162 // Unreachable code detected
                        break;
#pragma warning restore CS0162 // Unreachable code detected
                }
            }


            return new PassportData
            {
                BirthYear = birthYear,
                IssueYear = issueYear,
                ExpirationYear = expirationYear,
                Height = height,
                HairColor = hairColor,
                EyeColor = eyeColor,
                PassportId = passportId,
                CountryId = countryId,
            };
        }

        private static bool Part1Predicate(PassportData data)
        {
            bool valid = false;

            if (data.BirthYear != string.Empty &&
                data.IssueYear != string.Empty &&
                data.ExpirationYear != string.Empty &&
                data.Height != string.Empty &&
                data.HairColor != string.Empty &&
                data.EyeColor != string.Empty &&
                data.PassportId != string.Empty)
            {
                valid = true;
            }
            
            return valid;
        }

        private static bool Part2Predicate(PassportData data)
        {
            bool valid = false;

            if (data.ValidBirthYear() &&
                data.ValidIssueYear() &&
                data.ValidExpirationYear() &&
                data.ValidHeight() &&
                data.ValidHairColor() &&
                data.ValidEyeColor() &&
                data.ValidPassportId())
            {
                valid = true;
            }

            return valid;
        }

    }
}
