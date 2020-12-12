namespace AOC2020.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MapBuilder<T>
        where T : ISquareValue, new()
    {
        private readonly List<List<Square>> _squares = new ();

        private readonly List<string> _input = null;

        public MapBuilder(List<string> input)
        {
            _input = input;
        }

        public Map Build()
        {
            int currentRowNumber = 0;
            List<Square> previousRow = null;
            List<Square> currentRow = null;

            foreach (var row in _input)
            {
                currentRow = new ();

                for (int columnNumber = 0; columnNumber < row.Length; columnNumber++)
                {
                    var cell = row[columnNumber];
                    Square square = new Square(new Point(columnNumber, currentRowNumber), cell == '.' ? new EmptySquareValue() : new T());

                    if (previousRow != null)
                    {
                        Square above = previousRow[columnNumber];
                        above.SetDown(square);
                        square.SetUp(above);
                    }

                    if (columnNumber > 0)
                    {
                        Square left = currentRow[columnNumber - 1];
                        left.SetRight(square);
                        square.SetLeft(left);
                    }

                    if (columnNumber + 1 == row.Length)
                    {
                        Square beginning = currentRow[0];
                        beginning.SetLeft(square);
                        square.SetRight(beginning);
                    }

                    currentRow.Add(square);
                }

                currentRowNumber++;
                _squares.Add(currentRow);
                previousRow = currentRow;
            }

            int maxColumns = _squares.Max(x => x.Count);
            if (_squares.Any(x => x.Count != maxColumns))
            {
                throw new Exception("Unexpected column count in the list of squares for map");
            }

            Square[,] squares = new Square[_squares.Count, maxColumns];

            for (int row = 0; row < _squares.Count; row++)
            {
                for (int column = 0; column < maxColumns; column++)
                {
                    squares[row, column] = _squares[row][column];
                }
            }

            Map map = new Map(new Point(0, 0), squares);
            return map;
        }
    }
}
