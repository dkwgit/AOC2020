namespace AOC2020.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Map
    {
        private readonly Square[,] _squares = null;

        private readonly int _width = -1;

        private readonly bool _wrap = false;

#pragma warning disable IDE0052 // Remove unread private members
        private int _columnOffset = 0;
#pragma warning restore IDE0052 // Remove unread private members

        private Point _currentPoint = null;

        public Map(Point startPoint, Square[,] mapSquares, bool wrap)
        {
            _currentPoint = startPoint;
            _squares = mapSquares;
            _wrap = wrap;
            _width = mapSquares.GetLength(1);
        }

        public Point CurrentPoint => _currentPoint;

        public List<Square> Run((int, int) slope)
        {
            bool run = true;
            List<Square> path = new ();
            while (run)
            {
                _currentPoint = Move(slope, _currentPoint);
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

        class Change
        {
            public int I { get; init; }

            public int J { get; init; }

            public char NewValue { get; init; }
        }

        public void ChangeSquares(Func<Square, char> squareChanger)
        {
            //int[] indexArray1 = new int[_squares.GetLength(0) * _squares.GetLength(1)];
            //int[] indexArray2 = new int[_squares.GetLength(0) * _squares.GetLength(1)];
            //char[] newValues = new char[_squares.GetLength(0) * _squares.GetLength(1)];

            

        

             Change[] changes = new Change[_squares.GetLength(0) * _squares.GetLength(1)];


        int changeArrayIndex = 0;
            for (int i = 0; i < _squares.GetLength(0); i++)
            {
                for (int j = 0; j < _squares.GetLength(1); j++)
                {
                    var s = _squares[i, j];
                    if (s.Value != '.')
                    {
                        var newValue = squareChanger(s);
                        if (s.Value != newValue)
                        {
                            changes[changeArrayIndex] = new Change(){ I = i, J = j, NewValue = newValue };
                            changeArrayIndex++;
                        }
                    }
                }
            }

            for (int change = 0; change < changeArrayIndex; change++)
            {
                var o = changes[change];
                _squares[o.I, o.J].SetValue(o.NewValue);
            }
        }

        public List<string> GetTextRepresentation()
        {
            List<string> representation = new ();

            for (int i = 0; i < _squares.GetLength(0); i++)
            {
                StringBuilder sb = new ();
                for (int j = 0; j < _squares.GetLength(1); j++)
                {
                    Square s = _squares[i, j];
                    sb.Append(s.Value);
                }

                representation.Add(sb.ToString());
            }

            return representation;
        }

        public Square GetSquareFromPoint(Point p)
        {
            if ((p.Y < 0) || (p.Y + 1 > _squares.GetLength(0)))
            {
                return null;
            }

            return _squares[p.Y, p.X];
        }

        public Point Move((int, int) slope, Point inPoint)
        {
            Point newPoint = inPoint.PointFromOffset(slope);

            if ((newPoint.Y < 0) || (newPoint.Y + 1 > _squares.GetLength(0)))
            {
                return null;
            }

            if (!_wrap && ((newPoint.X < 0) || newPoint.X + 1 > _width))
            {
                return null;
            }

            if (_wrap && (newPoint.X + 1 > _width || newPoint.X < 0))
            {
                int offset = _width * ((slope.Item1 >= 0) ? 1 : -1);
                _columnOffset += offset;
                newPoint = newPoint.PointFromOffset(((-1) * offset, 0));
            }

            return newPoint;
        }
    }
}
