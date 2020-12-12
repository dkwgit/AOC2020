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
                new PuzzleData("10", "Actual", puzzleDataStore.GetPuzzleInputAsList("10"), puzzleDataStore.GetPuzzleAnswer("10", "1"), puzzleDataStore.GetPuzzleAnswer("10", "2")),

                // new PuzzleData("11", "Actual", puzzleDataStore.GetPuzzleInputAsList("11"), puzzleDataStore.GetPuzzleAnswer("11", "1"), puzzleDataStore.GetPuzzleAnswer("11", "2")),
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
                new PuzzleData(
                    "10",
                    "Sample",
                    new List<string>()
                    {
                        "1", "4", "5", "6", "7", "10", "11", "12", "15", "16", "19",
                    },
                    "35",
                    "8"),
                new PuzzleData(
                    "10",
                    "Sample",
                    new List<string>()
                    {
                        "28", "33", "18", "42", "31", "14", "46", "20", "48", "47", "24", "23", "49", "45", "19", "38", "39", "11", "1", "32", "25", "35", "8", "17", "7", "9", "4", "2", "34", "10", "3",
                    },
                    "220",
                    "19208"),
                /*new PuzzleData(
                    "11",
                    "Sample",
                    new List<string>()
                    {
                        "L.LL.LL.LL",
                        "LLLLLLL.LL",
                        "L.L.L..L..",
                        "LLLL.LL.LL",
                        "L.LL.LL.LL",
                        "L.LLLLL.LL",
                        "..L.L.....",
                        "LLLLLLLLLL",
                        "L.LLLLLL.L",
                        "L.LLLLL.LL",
                    },
                    "37",
                    ""),*/
            };
        }

        public List<PuzzleData> GetPuzzleData()
        {
            return _puzzleData.OrderBy(x => (x.Day, (x.Type == "Actual") ? 2 : 1)).ToList();
        }
    }
}
