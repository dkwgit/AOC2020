namespace AOC2020.Map
{
    public class Square
    {
        private readonly ISquareValue _value;

        private readonly Point _location = null;

        private Square _up = null;

        private Square _right = null;

        private Square _down = null;

        private Square _left = null;

        public Square(Point location, ISquareValue value)
        {
            _location = location;
            _value = value;
        }

        public Square Up => _up;

        public Square Right => _right;

        public Square Down => _down;

        public Square Left => _left;

        public Point OriginalLocation => _location;

        public bool Has(System.Type squareValue)
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
    }
}
