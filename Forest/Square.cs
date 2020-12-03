using System;
using System.Collections.Generic;
using System.Text;

namespace Forest
{
    class Square
    {
        private Square _up = null;
        private Square _right = null;
        private Square _down = null;
        private Square _left = null;
        private bool _tree = false;
        private int _x = -1;
        private int _y = -1;
        private int _totalX = -1;
        private int _totalY = -1;

        public Square Up => _up;
        public Square Right => _right;
        public Square Down => _down;
        public Square Left => _left;
        public bool HasTree => _tree;

        public int X => _x;
        public int Y => _y;
        public int TotalX => _totalX;
        public int TotalY => _totalY;

        public Square(int x, int y, bool tree)
        {
            _x =_totalX = x;
            _y =_totalY = y;
            _treee = tree;
        }
    }
}
