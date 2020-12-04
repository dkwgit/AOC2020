namespace AOC2020.Utilities
{
    using System.Collections.Generic;

    public interface IPuzzleDataStore
    {
        public List<string> GetPuzzleInputAsList(string dayNumber, bool suppressEmpty = true);

        public string GetPuzzleInput(string dayNumber);

        public string GetPuzzleAnswer(string dayNumber, string partNumber);
    }
}
