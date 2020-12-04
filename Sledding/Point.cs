namespace AOC2020.Sledding
{
    public record Point
    {
        public int X { get; init; }

        public int Y { get; init; }

        public Point(int x, int y) => (X, Y) = (x, y);

        public Point PointFromOffset((int, int) offset)
        {
            return new Point(X + offset.Item1, Y + offset.Item2);
        }
    }
}
