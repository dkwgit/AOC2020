using System;
using System.Collections.Generic;

namespace AOC2020.Sledding
{
    public class Forest
    {
        private int _columnOffset = 0;
        private Point _sledPoint = null;
        private readonly Square[,] _squares = null;
        private readonly int _width = -1;
        public Point SledPoint => _sledPoint;


        public Forest(Point sledPoint, Square[,] forestSquares)
        {
            _sledPoint = sledPoint;
            _squares = forestSquares;
            _width = forestSquares.GetLength(1);
        }

        public List<Square> Run((int,int) slope)
        {
            bool run = true;
            List<Square> path = new List<Square>();
            while (run)
            {
                _sledPoint = _sledPoint.AddOffset(slope);
                if (_sledPoint.X + 1 > _width || _sledPoint.X < 0)
                {
                    int offset = _width * ((slope.Item1 >= 0) ? (1) : (-1));
                    _columnOffset += offset;
                    _sledPoint = _sledPoint.AddOffset(((-1) * offset, 0));
                }
                Square s = _squares[_sledPoint.Y, _sledPoint.X];
                path.Add(s);
                if (s.Down == null)
                {
                    run = false;
                }
            }
            return path;
        }

        public void ResetForest(Point sledPoint)
        {
            _sledPoint = sledPoint;
            _columnOffset = 0;
        }
    }
}
