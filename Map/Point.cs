namespace AOC2020.Map
{
    public record Point
    {
        public int X { get; init; }

        public int Y { get; init; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point PointFromOffset(Point offsetPoint)
        {
            return new Point(X + offsetPoint.X, Y + offsetPoint.Y);
        }
    }
}
