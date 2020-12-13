namespace AOC2020.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Square
    {
        private readonly Point _location = null;

        private ISquareValue _value;

        private Square _up = null;

        private Square _right = null;

        private Square _down = null;

        private Square _left = null;

        private Square _upLeft = null;

        private Square _upRight = null;

        private Square _downLeft = null;

        private Square _downRight = null;

        public Square(Point location, ISquareValue value)
        {
            _location = location;
            _value = value;
        }

        public ISquareValue Value => _value;

        public Square Up => _up;

        public Square Right => _right;

        public Square Down => _down;

        public Square Left => _left;

        public Square UpLeft => _upLeft;

        public Square UpRight => _upRight;

        public Square DownLeft => _downLeft;

        public Square DownRight => _downRight;

        public Point Location => _location;

        public void SetValue(ISquareValue value)
        {
            _value = value;
        }

        public bool IsType(System.Type squareValue)
        {
            bool returnValue = squareValue.IsInstanceOfType(_value);
            return returnValue;
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
            return neighbors.Where(x => x is not null).ToList();
        }

        public List<Square> GetFirstValuesInMainDirection(Type t, Map map)
        {
            List<Square> foundSquares = new ();

            List<(int, int)> directions = new ()
            {
                (-1, -1),
                (0, -1),
                (1, -1),
                (1, 0),
                (1, 1),
                (0, 1),
                (-1, 1),
                (-1, 0),
            };

            foreach (var offset in directions)
            {
                bool seek = true;
                Square current = this;
                while (seek)
                {
                    Point p = map.Move(offset, current.Location);
                    if (p == null)
                    {
                        seek = false;
                        break;
                    }

                    current = map.GetSquareFromPoint(p);
                    if (current.IsType(t))
                    {
                        foundSquares.Add(current);
                        seek = false;
                    }
                }
            }

            return foundSquares;
        }
    }
}
