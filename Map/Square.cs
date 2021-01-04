namespace AOC2020.Map
{
    using System.Collections.Generic;
    using System.Linq;

    public class Square
    {
        private static Point[] _directions = new Point[8]
        {
            new Point(-1, -1),
            new Point(0, -1),
            new Point(1, -1),
            new Point(1, 0),
            new Point(1, 1),
            new Point(0, 1),
            new Point(-1, 1),
            new Point(-1, 0),
        };

        private readonly Point _location = null;

        private char _value;

        private Square _up = null;

        private Square _right = null;

        private Square _down = null;

        private Square _left = null;

        private Square _upLeft = null;

        private Square _upRight = null;

        private Square _downLeft = null;

        private Square _downRight = null;

        public Square(Point location, char value)
        {
            _location = location;
            _value = value;
        }

        public static Point[] Directions => _directions;

        public char Value => _value;

        public Square Up => _up;

        public Square Right => _right;

        public Square Down => _down;

        public Square Left => _left;

        public Square UpLeft => _upLeft;

        public Square UpRight => _upRight;

        public Square DownLeft => _downLeft;

        public Square DownRight => _downRight;

        public Point Location => _location;

        public void SetValue(char value)
        {
            _value = value;
        }

        public void SetDown(Square s)
        {
            _down = s;
        }

        public void SetUp(Square s)
        {
            _up = s;
        }

        public void SetRight(Square s)
        {
            _right = s;
        }

        public void SetLeft(Square s)
        {
            _left = s;
        }

        public void SetUpLeft(Square s)
        {
            _upLeft = s;
        }

        public void SetUpRight(Square s)
        {
            _upRight = s;
        }

        public void SetDownRight(Square s)
        {
            _downRight = s;
        }

        public void SetDownLeft(Square s)
        {
            _downLeft = s;
        }

        public List<Square> GetNeighbors()
        {
            List<Square> neighbors = new ()
            {
                UpLeft,
                Up,
                UpRight,
                Right,
                DownRight,
                Down,
                DownLeft,
                Left,
            };

            return neighbors;
        }

        public List<Square> GetFirstValuesInMainDirection(char valueToIgnore, Map map)
        {
            List<Square> foundSquares = new (8);

            for (int i = 0; i < Directions.Length; i++)
            {
                Point point = Directions[i];
                Square current = this;
                while (true)
                {
                    Point p = map.Move(point, current.Location);
                    if (p == null)
                    {
                        break;
                    }

                    current = map.GetSquareFromPoint(p);
                    if (valueToIgnore != current.Value)
                    {
                        foundSquares.Add(current);
                        break;
                    }
                }
            }

            return foundSquares;
        }
    }
}
