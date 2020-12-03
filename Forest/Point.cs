using System;
using System.Collections.Generic;
using System.Text;

namespace Forest
{
    public class Point
    {
        readonly int _x = -1;
        readonly int _y = -1;

        public int X => _x;
        public int Y => _y;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}
