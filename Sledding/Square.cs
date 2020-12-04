using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020.Sledding
{
    public class Square
    {
        private Square _up = null;
        private Square _right = null;
        private Square _down = null;
        private Square _left = null;
        private readonly bool _tree = false;
        private Point _location = null;

        public Square Up => _up;
        public Square Right => _right;
        public Square Down => _down;
        public Square Left => _left;
        public bool HasTree => _tree;

        public Point OriginalLocation => _location;
        public Square(Point location, bool tree)
        {
            _location = location;
            _tree = tree;
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
    }
}
