using System;
using System.Reflection;
using System.Collections.Generic;
using System.Resources;


namespace AOC2020.Utilities
{
    public class PuzzleDataStore : IPuzzleDataStore
    {
        public List<string> GetPuzzleInputAsList(string dayNumber)
        {
            List<string> list = new List<string>(1000);
            string input = GetPuzzleInput(dayNumber).Trim();

            using (System.IO.StringReader reader = new System.IO.StringReader(input))
            {
                string line = "";
                line = reader.ReadLine();
                do
                {
                    if (string.Empty.CompareTo(line) != 0)
                    {
                        list.Add(line);
                    }
                    line = reader.ReadLine();
                } while (null != line);
            }
            return list;
        }
        public string GetPuzzleInput(string dayNumber)
        {
            Type resources = typeof(Resources);
            PropertyInfo propertyToGet = resources.GetProperty($"Day{dayNumber}_PuzzleInput", BindingFlags.Static | BindingFlags.NonPublic);
            string input = propertyToGet.GetValue(null) as string;
            return input;
        }

        public string GetPuzzleAnswer(string dayNumber, string PartNumber)
        {
            ResourceManager rm = new ResourceManager(typeof(Resources));
            return rm.GetString($"Day{dayNumber}Part{PartNumber}_Answer");
        }
    }
}
