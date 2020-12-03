using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2020.Sledding
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

        public Point AddOffset((int,int) offset)
        {
            return new Point(X + offset.Item1, Y + offset.Item2);
        }
    }
}
