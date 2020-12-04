namespace AOC2020.Sledding
{
    public class Square
    {
        private readonly bool _tree = false;

        private readonly Point _location = null;

        private Square _up = null;

        private Square _right = null;

        private Square _down = null;

        private Square _left = null;

        public Square(Point location, bool tree)
        {
            _location = location;
            _tree = tree;
        }

        public Square Up => _up;

        public Square Right => _right;

        public Square Down => _down;

        public Square Left => _left;

        public bool HasTree => _tree;

        public Point OriginalLocation => _location;

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
