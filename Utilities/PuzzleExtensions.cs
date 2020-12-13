namespace AOC2020.Utilities
{
    using System;

    public static class PuzzleExtensions
    {
        public static void RegressionTest(this IPuzzle puzzle, PuzzleData testData)
        {
            puzzle.ProcessPuzzleInput(testData.Input);

            string part1StoredAnswer = testData.AnswerPart1;
            string part2StoredAnswer = testData.AnswerPart2;

            string part1Answer = puzzle.Part1;
            string part2Answer = (part2StoredAnswer != string.Empty) ? puzzle.Part2 : string.Empty;

            if (part1StoredAnswer != null)
            {
                if (part1Answer != part1StoredAnswer)
                {
                    throw new Exception($"Day{puzzle.Day}, Part1 gave answer of {part1Answer}, but expected answer was {part1StoredAnswer}");
                }
            }
            else
            {
                throw new Exception($"Day{puzzle.Day}, could not find answer for Part1 in the data store.");
            }

            if (part2StoredAnswer != null)
            {
                if (part2Answer != part2StoredAnswer)
                {
                    throw new Exception($"Day{puzzle.Day}, Part1 gave answer of {part2Answer}, but expected answer was {part2StoredAnswer}");
                }
            }
            else
            {
                // throw new Exception($"Day{puzzle.Day}, could not find answer for Part1 in the data store.");
            }
        }
    }
}
