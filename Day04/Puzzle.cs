namespace AOC2020.Day04
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly List<PassportData> _passportData = new ();

        private List<string> _input;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                string answer = _passportData.Count(x => x.HasAllRequiredFields()).ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} passports that were valid of total {count}", Day, answer, _passportData.Count);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = _passportData.Count(x => x.HasAllRequiredFields() && x.ValidateFields()).ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} passports that were valid for part2 out of {count}", Day, answer, _passportData.Count);
                return answer;
            }
        }

        public string Day => "04";

        public void ProcessPuzzleInput()
        {
            _input = new PuzzleDataStore().GetPuzzleInputAsList(Day, false);
            List<string> dataItems = new ();

            for (int index = 0; index < _input.Count; index++)
            {
                string line = _input[index].Trim();

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
    }
}
