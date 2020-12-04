﻿namespace AOC2020.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Extensions
    {
        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out IList<T> rest)
        {
            first = list.Count > 0 ? list[0] : default; // or throw
            second = list.Count > 1 ? list[1] : default; // or throw
            rest = list.Skip(2).ToList();
        }
    }
}
