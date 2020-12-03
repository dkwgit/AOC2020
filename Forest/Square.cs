using System;
using System.Collections.Generic;
using System.Text;

namespace Forest
{
    public class Square
    {
        private Square _up = null;
        private Square _right = null;
        private Square _down = null;
        private Square _left = null;
        private readonly bool _tree = false;
        private Point _originalLocation = null;
        private Point _wrappedAroundLocation = null;

        public Square Up => _up;
        public Square Right => _right;
        public Square Down => _down;
        public Square Left => _left;
        public bool HasTree => _tree;

        public Point OriginalLocation => _originalLocation;
        public Point WrappedAroundLocation => _wrappedAroundLocation;
        public Square(Point original, bool tree)
        {
            _originalLocation = original;
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
