namespace AOC2020.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Resources;

    public class PuzzleDataStore : IPuzzleDataStore
    {
        public List<string> GetPuzzleInputAsList(string dayNumber)
        {
            List<string> list = new List<string>(1000);
            string input = GetPuzzleInput(dayNumber).Trim();

            using (System.IO.StringReader reader = new System.IO.StringReader(input))
            {
                string line = string.Empty;
                line = reader.ReadLine();
                do
                {
                    list.Add(line);

                    line = reader.ReadLine();
                }
                while (line != null);
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

        public string GetPuzzleAnswer(string dayNumber, string partNumber)
        {
            ResourceManager rm = new ResourceManager(typeof(Resources));
            return rm.GetString($"Day{dayNumber}Part{partNumber}_Answer");
        }
    }
}
