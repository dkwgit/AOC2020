﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AOC2020.Sledding
{
    public class ForestBuilder
    {
        private readonly List<List<Square>> _forestSquares = new List<List<Square>>();
        private readonly List<String> _input = null;
        public ForestBuilder(List<String> input)
        {
            _input = input;
        }

        public Forest Build()
        {
            int currentRowNumber = 0;
            List<Square> previousRow = null;
            List<Square> currentRow = null;

            foreach(var row in _input)
            {
                currentRow = new List<Square>();
                for (int columnNumber = 0; columnNumber < row.Length; columnNumber++)
                {
                    var cell = row[columnNumber];
                    Square square = new Square(new Point(columnNumber, currentRowNumber), cell == '#');
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
                _forestSquares.Add(currentRow);
                previousRow = currentRow;
            }

            int maxColumns = _forestSquares.Max(x => x.Count);
            if (_forestSquares.Any(x=>x.Count != maxColumns))
            {
                throw new Exception("Unexpected column count in the list of squares for forest");
            }

            Square[,] squares = new Square[_forestSquares.Count, maxColumns];

            for (int row = 0; row < _forestSquares.Count; row++)
            {
                for(int column = 0; column < maxColumns; column++)
                {
                    squares[row, column] = _forestSquares[row][column];
                }
            }


            Forest forest = new Forest(new Point(0, 0), squares);
            return forest;
        }
    }
}