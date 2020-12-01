using System;
using System.Reflection;

namespace AOC2020Library
{
    public class ResourceLoader
    {
        public static string GetPuzzleInput(string dayNumber)
        {
            Type resources = typeof(Resources);
            PropertyInfo propertyToGet = resources.GetProperty($"Day{dayNumber}_PuzzleInput", BindingFlags.Static | BindingFlags.NonPublic);
            string input = propertyToGet.GetValue(null) as string;
            return input;
        }
    }
}
