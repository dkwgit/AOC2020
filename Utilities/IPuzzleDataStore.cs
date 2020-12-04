using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020.Utilities
{
    public interface IPuzzleDataStore
    {
        public List<string> GetPuzzleInputAsList(string dayNumber);
        public string GetPuzzleInput(string dayNumber);

        public string GetPuzzleAnswer(string dayNumber, string PartNumber);
    }
}
