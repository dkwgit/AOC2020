namespace AOC2020.Map
{
    using System.Collections.Generic;

    public class Map
    {
        private readonly Square[,] _squares = null;

        private readonly int _width = -1;

#pragma warning disable IDE0052 // Remove unread private members
        private int _columnOffset = 0;
#pragma warning restore IDE0052 // Remove unread private members

        private Point _currentPoint = null;

        public Map(Point startPoint, Square[,] mapSquares)
        {
            _currentPoint = startPoint;
            _squares = mapSquares;
            _width = mapSquares.GetLength(1);
        }

        public Point CurrentPoint => _currentPoint;

        public List<Square> Run((int, int) slope)
        {
            bool run = true;
            List<Square> path = new ();
            while (run)
            {
                MoveOnce(slope);
                Square s = _squares[_currentPoint.Y, _currentPoint.X];
                path.Add(s);
                if (s.Down == null)
                {
                    run = false;
                }
            }

            return path;
        }

        public void ResetMap(Point point)
        {
            _currentPoint = point;
            _columnOffset = 0;
        }

        private void MoveOnce((int, int) slope)
        {
            _currentPoint = _currentPoint.PointFromOffset(slope);
            if (_currentPoint.X + 1 > _width || _currentPoint.X < 0)
            {
                int offset = _width * ((slope.Item1 >= 0) ? 1 : -1);
                _columnOffset += offset;
                _currentPoint = _currentPoint.PointFromOffset(((-1) * offset, 0));
            }
        }
    }
}
