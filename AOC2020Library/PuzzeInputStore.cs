using System;
using System.Reflection;
using System.Collections.Generic;


namespace AOC2020Library
{
    public class PuzzleInputStore
    {
        public static List<string> GetPuzzleInputList(string dayNumber)
        {
            List<string> list = new List<string>(1000);
            string input = GetPuzzleInput(dayNumber).Trim();

            using (System.IO.StringReader reader = new System.IO.StringReader(input))
            {
                while (reader.Peek() != -1)
                {
                    string line = reader.ReadLine();
                    if (line.Trim().Length > 0)
                    {
                        list.Add(line);
                    }
                }
            }
            return list;
        }
        public static string GetPuzzleInput(string dayNumber)
        {
            Type resources = typeof(Resources);
            PropertyInfo propertyToGet = resources.GetProperty($"Day{dayNumber}_PuzzleInput", BindingFlags.Static | BindingFlags.NonPublic);
            string input = propertyToGet.GetValue(null) as string;
            return input;
        }
    }
}
