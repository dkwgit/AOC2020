namespace AOC2020.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;

    internal class Picture : IPicture
    {
        private readonly Dictionary<Point, char> _occupiedPoints = new ();

        private readonly Dictionary<Point, char> _serpentPoints = new ();

        private Registry _registry;

        private int _tilesPerSide;

        private int _tileSideLengthWithBorder;

        private int _tileSideLengthWithoutBorder;

        private int _pictureWidth;

        private int _pictureHeight;

        private int[,] _tiles;

        private char[,] _finalPictureData;

        public IPicture Assemble()
        {
            _tiles = new int[_tilesPerSide, _tilesPerSide];

            for (int row = 0; row < _tilesPerSide; row++)
            {
                for (int column = 0; column < _tilesPerSide; column++)
                {
                    int tileId;
                    if (column == 0)
                    {
                        if (row == 0)
                        {
                            tileId = PickAndOrientStartingCorner();
                        }
                        else
                        {
                            tileId = PickAndOrientNeighborByEdge(_tiles[row - 1, column], Tile.EdgeEnum.Bottom, Tile.EdgeEnum.Top);
                        }
                    }
                    else
                    {
                        tileId = PickAndOrientNeighborByEdge(_tiles[row, column - 1], Tile.EdgeEnum.Right, Tile.EdgeEnum.Left);
                    }

                    _tiles[row, column] = tileId;
                }
            }

            return this;
        }

        public int FindPatterns(Pattern pattern, int numberToFind = -1)
        {
            int totalNumberOfVerticalMoves = _pictureHeight - pattern.PatternHeight;
            int totalNumberOfHorizontalMoves = _pictureWidth - pattern.PatternWidth;

            int numberFound = 0;
            for (int verticalMove = 0; verticalMove <= totalNumberOfVerticalMoves; verticalMove++)
            {
                for (int horizontalMove = 0; horizontalMove <= totalNumberOfHorizontalMoves; horizontalMove++)
                {
                    List<Point> patternPoints = pattern.GetPatternWithOffset(new Point() { Y = verticalMove, X = horizontalMove });
                    if (patternPoints.All(p => _occupiedPoints.ContainsKey(p)))
                    {
                        foreach (var p in patternPoints)
                        {
                            if (!_serpentPoints.ContainsKey(p))
                            {
                                _serpentPoints.Add(p, '#');
                            }
                        }

                        numberFound++;
                        if (numberFound == numberToFind)
                        {
                            return numberFound;
                        }
                    }
                }
            }

            return numberFound;
        }

        public IPicture FlipOnXAxis()
        {
            _finalPictureData = Tile.FlipAlongX(_pictureWidth, _finalPictureData);
            ResetOccupiedPoints();
            return this;
        }

        public IPicture RotateRight()
        {
            _finalPictureData = Tile.RotateRightOnce(_finalPictureData, _pictureWidth);
            ResetOccupiedPoints();
            return this;
        }

        public IPicture SetLengths(int tileSideLengthWithBorder, int tileSideLengthWithoutBorder, int tilesPerSide)
        {
            (_tileSideLengthWithBorder, _tileSideLengthWithoutBorder, _tilesPerSide) = (tileSideLengthWithBorder, tileSideLengthWithoutBorder, tilesPerSide);
            return this;
        }

        public void SetPatternMatchForPoint(Point p)
        {
            throw new System.NotImplementedException();
        }

        public IPicture SetRegistry(Registry registry)
        {
            _registry = registry;
            return this;
        }

        public IPicture StripBorders()
        {
            _pictureHeight = _tileSideLengthWithoutBorder * _tilesPerSide;
            _pictureWidth = _tileSideLengthWithoutBorder * _tilesPerSide;
            _finalPictureData = new char[_pictureHeight, _pictureWidth];

            for (int pictureRow = 0; pictureRow < _tilesPerSide; pictureRow++)
            {
                for (int pictureColumn = 0; pictureColumn < _tilesPerSide; pictureColumn++)
                {
                    Tile t = _registry.Tiles[_tiles[pictureRow, pictureColumn]];
                    for (int row = 1; row <= _tileSideLengthWithoutBorder; row++)
                    {
                        for (int column = 1; column <= _tileSideLengthWithoutBorder; column++)
                        {
                            int pictureDataRowIndex = (pictureRow * _tileSideLengthWithoutBorder) + row - 1;
                            int pictureDataColumnIndex = (pictureColumn * _tileSideLengthWithoutBorder) + column - 1;
                            char ch = t.FinalOrientation[row, column];
                            if (ch == '#')
                            {
                                _occupiedPoints.Add(new Point() { Y = pictureDataRowIndex, X = pictureDataColumnIndex }, ch);
                            }

                            _finalPictureData[pictureDataRowIndex, pictureDataColumnIndex] = ch;
                        }
                    }
                }
            }

            return this;
        }

        public void PrintPictureFromTiles()
        {
            List<string> pictureRows = new ();

            for (int row = 0; row < _tilesPerSide; row++)
            {
                List<StringBuilder> builders = new ();
                for (int i = 0; i < _tileSideLengthWithBorder; i++)
                {
                    builders.Add(new StringBuilder());
                }

                for (int column = 0; column < _tilesPerSide; column++)
                {
                    Tile t = _registry.Tiles[_tiles[row, column]];
                    List<string> tileText = Tile.GetTileText(t.FinalOrientation, t.Length);
                    Debug.Assert(t.Length == _tileSideLengthWithBorder, "Expecting same values stored in Tile.Length and Picture._tileSideLengthWithBorder");

                    for (int i = 0; i < _tileSideLengthWithBorder; i++)
                    {
                        builders[i].Append(tileText[i]);
                        builders[i].Append(' ');
                    }
                }

                for (int i = 0; i < _tileSideLengthWithBorder; i++)
                {
                    pictureRows.Add(builders[i].ToString());
                }

                pictureRows.Add(string.Empty);
            }

            Tile.PrintTile(pictureRows);
        }

        public int GetOccupiedPointCount()
        {
            return _occupiedPoints.Count;
        }

        public int GetSerpentPointCount()
        {
            return _serpentPoints.Count;
        }

        private void ResetOccupiedPoints()
        {
            _occupiedPoints.Clear();
            for (int row = 0; row < _pictureHeight; row++)
            {
                for (int column = 0; column < _pictureWidth; column++)
                {
                    char ch = _finalPictureData[row, column];
                    if (ch == '#')
                    {
                        _occupiedPoints.Add(new Point() { Y = row, X = column }, ch);
                    }
                }
            }
        }

        private int PickAndOrientStartingCorner()
        {
            var cornerInfo = _registry.TileToNeighborTilesWithEdge.Where(x => x.Value.Count == 4).First();
            int cornerId = cornerInfo.Key;
            string oneEdge = cornerInfo.Value.Select(x => x.edge).First();
            var edgeInfo = _registry.EdgeToTiles[oneEdge];
            int matchingIndex = edgeInfo.Where(x => x.tileId == cornerId).First().matchingIndex;
            var result = _registry.EdgeToTiles.Where(x => x.Key != oneEdge && x.Value.Any(y => y.tileId == cornerId && y.matchingIndex == matchingIndex)).Select(z => z.Key).ToList();
            string otherEdge = _registry.TileToNeighborTilesWithEdge[cornerId].Where(x => result.Any(e => e == x.edge)).Select(y => y.edge).Single();
            Tile t = _registry.Tiles[cornerId];
            Tile.EdgeEnum one = t.MatchEdge(oneEdge, matchingIndex);
            Tile.EdgeEnum other = t.MatchEdge(otherEdge, matchingIndex);
            if (one == Tile.EdgeEnum.Invalid || other == Tile.EdgeEnum.Invalid)
            {
                throw new InvalidOperationException("Unpexected invalid value return from edge match");
            }

            char[,] data = t.Data[matchingIndex];
            int rotationCount;
            if ((one == Tile.EdgeEnum.Bottom && other == Tile.EdgeEnum.Right) || (one == Tile.EdgeEnum.Right && other == Tile.EdgeEnum.Bottom))
            {
                rotationCount = 0;
            }
            else if (one == Tile.EdgeEnum.Bottom || other == Tile.EdgeEnum.Bottom)
            {
                rotationCount = 3;
            }
            else if ((one == Tile.EdgeEnum.Left && other == Tile.EdgeEnum.Top) || (one == Tile.EdgeEnum.Top && other == Tile.EdgeEnum.Left))
            {
                rotationCount = 2;
            }
            else if ((one == Tile.EdgeEnum.Right && other == Tile.EdgeEnum.Top) || (one == Tile.EdgeEnum.Top && other == Tile.EdgeEnum.Right))
            {
                rotationCount = 1;
            }
            else
            {
                throw new InvalidOperationException("Unexpected combination of corner edges");
            }

            if (rotationCount != 0)
            {
                data = Tile.RotateRight(data, rotationCount, t.Length);
            }

            t.FinalOrientation = data;

            // Tile.PrintTile(Tile.GetTileText(data, t.Length));
            return cornerId;
        }

        private int PickAndOrientNeighborByEdge(int sourceTileId, Tile.EdgeEnum edgeOfSourceTile, Tile.EdgeEnum edgeOfTargetTile)
        {
            Tile sourceTile = _registry.Tiles[sourceTileId];

            // Tile.PrintTile(Tile.GetTileText(sourceTile.FinalOrientation, sourceTile.Length));
            string edgeToFind = new string(sourceTile.GetEdge(edgeOfSourceTile).ToCharArray().Reverse().ToArray());
            (int targetTileId, _) = _registry.TileToNeighborTilesWithEdge[sourceTile.Id].Where(x => x.edge == edgeToFind).Single();
            int matchingIndex = _registry.EdgeToTiles[edgeToFind].Where(x => x.tileId == targetTileId).Select(y => y.matchingIndex).Single();

            Tile target = _registry.Tiles[targetTileId];
            char[,] data = target.Data[matchingIndex];
            Tile.EdgeEnum edgeCurrent = target.MatchEdge(edgeToFind, matchingIndex);

            if (edgeCurrent != edgeOfTargetTile)
            {
                int rotationCount;
                if (edgeCurrent < edgeOfTargetTile)
                {
                    rotationCount = edgeOfTargetTile - edgeCurrent;
                }
                else
                {
                    rotationCount = (int)(edgeCurrent - edgeOfTargetTile) switch
                    {
                        1 => 3,
                        2 => 2,
                        3 => 1,
                        _ => throw new InvalidOperationException("Unexpected value diff between edges"),
                    };
                }

                data = Tile.RotateRight(data, rotationCount, target.Length);
            }

            target.FinalOrientation = data;

            return targetTileId;
        }
    }
}
