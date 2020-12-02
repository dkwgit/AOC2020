using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AOC2020Library
{
    public class RegressionTests
    {
        public static void RunRegressionTests(List<IPuzzle> puzzles)
        {
            /* var dayTypes = AppDomain.CurrentDomain.GetAssemblies().
                Where(a => a.ExportedTypes.Any(t => t.Name == "Puzzle")).
                Select(a => new { Assembly = a, TypeInfo = a.ExportedTypes.Where(t => t.Name == "Puzzle").Select(t => t).First() }).
                OrderBy(x => x.TypeInfo.Name).
                ToList();
            */

            foreach(var puzzle in puzzles)
            {
                
                puzzle.SetInput(PuzzleDataStore.GetPuzzleInputList(puzzle.Day));
                string part1Answer = puzzle.Part1;
                string part2Answer = puzzle.Part2;
                string part1StoredAnswer = PuzzleDataStore.GetPuzzleAnswer(puzzle.Day, "1");
                string part2StoredAnswer = PuzzleDataStore.GetPuzzleAnswer(puzzle.Day, "2");

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
                    throw new Exception($"Day{puzzle.Day}, could not find answer for Part1 in the data store.");
                }
            }
        }
    }
}
