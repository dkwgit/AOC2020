namespace AOC2020.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    public class DataFixture
    {
        private readonly List<PuzzleData> _puzzleData;

        public DataFixture()
        {
            PuzzleDataStore puzzleDataStore = new PuzzleDataStore();

            _puzzleData = new ()
            {
                new PuzzleData("01", "Actual", puzzleDataStore.GetPuzzleInputAsList("01"), puzzleDataStore.GetPuzzleAnswer("01", "1"), puzzleDataStore.GetPuzzleAnswer("01", "2")),
                new PuzzleData("02", "Actual", puzzleDataStore.GetPuzzleInputAsList("02"), puzzleDataStore.GetPuzzleAnswer("02", "1"), puzzleDataStore.GetPuzzleAnswer("02", "2")),
                new PuzzleData("03", "Actual", puzzleDataStore.GetPuzzleInputAsList("03"), puzzleDataStore.GetPuzzleAnswer("03", "1"), puzzleDataStore.GetPuzzleAnswer("03", "2")),
                new PuzzleData("04", "Actual", puzzleDataStore.GetPuzzleInputAsList("04"), puzzleDataStore.GetPuzzleAnswer("04", "1"), puzzleDataStore.GetPuzzleAnswer("04", "2")),
                new PuzzleData("05", "Actual", puzzleDataStore.GetPuzzleInputAsList("05"), puzzleDataStore.GetPuzzleAnswer("05", "1"), puzzleDataStore.GetPuzzleAnswer("05", "2")),
                new PuzzleData("06", "Actual", puzzleDataStore.GetPuzzleInputAsList("06"), puzzleDataStore.GetPuzzleAnswer("06", "1"), puzzleDataStore.GetPuzzleAnswer("06", "2")),
                new PuzzleData("07", "Actual", puzzleDataStore.GetPuzzleInputAsList("07"), puzzleDataStore.GetPuzzleAnswer("07", "1"), puzzleDataStore.GetPuzzleAnswer("07", "2")),
                new PuzzleData("08", "Actual", puzzleDataStore.GetPuzzleInputAsList("08"), puzzleDataStore.GetPuzzleAnswer("08", "1"), puzzleDataStore.GetPuzzleAnswer("08", "2")),
                new PuzzleData("09", "Actual", puzzleDataStore.GetPuzzleInputAsList("09"), puzzleDataStore.GetPuzzleAnswer("09", "1"), puzzleDataStore.GetPuzzleAnswer("09", "2")),
                new PuzzleData(
                    "08",
                    "Sample",
                    new List<string>()
                    {
                        "nop +0",
                        "acc +1",
                        "jmp +4",
                        "acc +3",
                        "jmp -3",
                        "acc -99",
                        "acc +1",
                        "jmp -4",
                        "acc +6",
                    },
                    "5",
                    string.Empty),
            };
        }

        public List<PuzzleData> GetPuzzleData(string day)
        {
            return _puzzleData.Where(x => x.Day == day).OrderBy(x => (x.Type == "Actual") ? 2 : 1).ToList();
        }
    }
}
