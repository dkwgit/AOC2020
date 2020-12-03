using System;
using System.Collections.Generic;

namespace AOC2020.Sledding
{
    public class Forest
    {
        private Point _sledPoint = null;
        private readonly List<List<Square>> _squares = null;
        public Point SledPoint => _sledPoint;
        
        
        public Forest(Point sledPoint, List<List<Square>> forestSquares)
        {
            _sledPoint = sledPoint;
            _squares = forestSquares;
        }
    }
}
