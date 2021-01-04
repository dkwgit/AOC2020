namespace AOC2020.Day24
{
    using System;
    using System.Collections.Generic;

    internal class TileManager
    {
        private static List<int> _ringCountList = new ();

        private Tile[] _tiles;

        private int _maxRing = -1;

        private int _maxVisitedRing = -1;

        private int _maxBlackRing = -1;

        public TileManager(int initialMaxRing)
        {
            Resize(initialMaxRing);
        }

        /*
         * Leaving in place because it documents directions for me
         *
         * public static List<(int xOffset, int yOffset, int zOffset)> DirectionOffsets { get; } = new ()
        {
            (0, 1, -1), // Arbitrarily, NW corner where x = 0, y = 1, z = -1 and all add to 0.  See cube coordinates for hexaxgons at https://www.redblobgames.com/grids/hexagons/
            (1, 0, -1), // NE
            (1, -1, 0), // E
            (0, -1, 1), // SE
            (-1, 0, 1), // SW
            (-1, 1, 0), // W
        }; */

        public int MaxRing
        {
            get
            {
                return _maxRing;
            }

            set
            {
                if (value > _maxRing)
                {
                    Resize(value);
                }
                else if (value < _maxRing)
                {
                    throw new InvalidOperationException("Unexpected max ring size smaller than current size");
                }
            }
        }

        public int MaxBlackRing
        {
            get
            {
                return _maxBlackRing;
            }

            set
            {
                _maxBlackRing = value;
            }
        }

        public int MaxVisitedRing => _maxVisitedRing;

        public static int TileCountByRing(int ring)
        {
            if (ring < _ringCountList.Count)
            {
                return _ringCountList[ring];
            }

            if (ring == 0)
            {
                _ringCountList.Add(1);
                return 1;
            }

            int result = (ring * 6) + TileCountByRing(ring - 1);
            if (ring == _ringCountList.Count)
            {
                _ringCountList.Add(result);
            }

            return result;
        }

        public int GetTileNumber(TileManager manager, int x, int y, int z)
        {
            int ring = Tile.GetRing(x, y, z);
            if (_maxVisitedRing < ring)
            {
                _maxVisitedRing = ring;
            }

            if (ring == 0)
            {
                return 0;
            }

            if (ring > _maxRing)
            {
                Resize(ring * 2);
            }

            int baseNumber = TileCountByRing(ring - 1);
            int tileNumber = (Math.Sign(x), Math.Sign(y), Math.Sign(z)) switch
            {
                (0, 1, -1)  => baseNumber,
                (1, 1, -1)  => baseNumber + x,
                (1, 0, -1)  => baseNumber + 1 + (1 * (ring - 1)),
                (1, -1, -1) => baseNumber + 1 + (1 * (ring - 1)) + Math.Abs(y),
                (1, -1, 0)  => baseNumber + 2 + (2 * (ring - 1)),
                (1, -1, 1)  => baseNumber + 2 + (2 * (ring - 1)) + z,
                (0, -1, 1)  => baseNumber + 3 + (3 * (ring - 1)),
                (-1, -1, 1) => baseNumber + 3 + (3 * (ring - 1)) + Math.Abs(x),
                (-1, 0, 1)  => baseNumber + 4 + (4 * (ring - 1)),
                (-1, 1, 1)  => baseNumber + 4 + (4 * (ring - 1)) + y,
                (-1, 1, 0)  => baseNumber + 5 + (5 * (ring - 1)),
                (-1, 1, -1) => baseNumber + 5 + (5 * (ring - 1)) + Math.Abs(z),
                _ => throw new InvalidOperationException("Unxpected tuple for tile number calculation"),
            };

            return tileNumber;
        }

        public Tile GetTile(int x, int y, int z)
        {
            int tileNumber = GetTileNumber(this, x, y, z);
            Tile t = _tiles[tileNumber];
            t.X = x;
            t.Y = y;
            t.Z = z;
            return t;
        }

        public int GetBlackTileCount()
        {
            int blackTileCount = 0;

            int tileCount = TileCountByRing(_maxRing);
            for (int i = 0; i < tileCount; i++)
            {
                if (_tiles[i].Color == 'b')
                {
                    blackTileCount++;
                }
            }

            return blackTileCount;
        }

        public void DoTileFlipping()
        {
            int tilesToVisit = TileCountByRing(MaxBlackRing + 1);
            int[] tilesToFlip = new int[tilesToVisit];
            int flipArrayIndex = 0;
            for (int i = 0; i < tilesToVisit; i++)
            {
                Tile tile = _tiles[i];
                int neighborBlackTileCount = tile.GetNeigborBlackTileCount();
                if (tile.Color == 'b' && (neighborBlackTileCount == 0 || neighborBlackTileCount > 2))
                {
                    tilesToFlip[flipArrayIndex] = i;
                    flipArrayIndex++;
                }

                if (tile.Color == 'w' && neighborBlackTileCount == 2)
                {
                    tilesToFlip[flipArrayIndex] = i;
                    flipArrayIndex++;
                }
            }

            for (int j = 0; j < flipArrayIndex; j++)
            {
                _tiles[tilesToFlip[j]].Flip();
            }
        }

        private void Resize(int newMaxRing)
        {
            int currentMaxRing = _maxRing;
            _maxRing = newMaxRing;
            int currentTileCount = currentMaxRing == -1 ? 0 : TileCountByRing(currentMaxRing);
            int newTileCount = TileCountByRing(_maxRing);
            Tile[] temp = new Tile[newTileCount];
            if (currentTileCount > 0)
            {
                Array.Copy(_tiles, temp, currentTileCount);
            }

            _tiles = temp;
            AllocateTiles(currentTileCount, newTileCount);
        }

        private void AllocateTiles(int startIndex, int count)
        {
            for (int i = startIndex; i < count; i++)
            {
                _tiles[i] = new Tile(this, i);
            }
        }
    }
}
