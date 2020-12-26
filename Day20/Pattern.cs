namespace AOC2020.Day20
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    internal class Pattern
    {
        public int PatternWidth { get; } = 20;

        public int PatternHeight { get; } = 3;

        public List<Point> BasePattern { get; } = new List<Point>()
        {
            new Point() { Y = 0, X = 18, },
            new Point() { Y = 1, X = 0, },
            new Point() { Y = 1, X = 5, },
            new Point() { Y = 1, X = 6, },
            new Point() { Y = 1, X = 11, },
            new Point() { Y = 1, X = 12, },
            new Point() { Y = 1, X = 17, },
            new Point() { Y = 1, X = 18, },
            new Point() { Y = 1, X = 19, },
            new Point() { Y = 2, X = 1, },
            new Point() { Y = 2, X = 4, },
            new Point() { Y = 2, X = 7, },
            new Point() { Y = 2, X = 10, },
            new Point() { Y = 2, X = 13, },
            new Point() { Y = 2, X = 16, },
        };

        public List<Point> GetPatternWithOffset(Point offset)
        {
            return BasePattern.Select(p => new Point() { Y = p.Y + offset.Y, X = p.X + offset.X }).ToList();
        }
    }
}
