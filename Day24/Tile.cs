namespace AOC2020.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Tile
    {
        public Tile((int x, int y, int z) location, Dictionary<(int x, int y, int z), Tile> allTiles)
        {
            (X, Y, Z) = location;
            AllTiles = allTiles;
            if (!AllTiles.ContainsKey(location))
            {
                AllTiles.Add(location, this);
            }
        }

        public static List<(int xOffset, int yOffset, int zOffset)> DirectionOffsets { get; } = new ()
        {
            (1, -1, 0),
            (0, -1, 1),
            (-1, 0, 1),
            (-1, 1, 0),
            (0, 1, -1),
            (1, 0, -1),
        };

        public Dictionary<(int x, int y, int z), Tile> AllTiles { get; } = new ();

        public int X { get; init; }

        public int Y { get; init; }

        public int Z { get; init; }

        public char Color { get; set; } = 'w';

        public int Ring
        {
            get
            {
                List<int> allDimensions = new ()
                {
                    Math.Abs(X),
                    Math.Abs(Y),
                    Math.Abs(Z),
                };
                return allDimensions.Max();
            }
        }

        public static void ProcessMoves(Tile currentTile, string moves)
        {
            string originalMoveString = moves;
            bool process = true;
            while (process)
            {
                string move = string.Empty;

                if ((moves[0] == 's') || (moves[0] == 'n'))
                {
                    move = moves.Substring(0, 2);
                    if (moves.Length > 2)
                    {
                        moves = moves[2..];
                    }
                    else
                    {
                        moves = string.Empty;
                    }
                }
                else
                {
                    move = moves[0].ToString();
                    if (moves.Length > 1)
                    {
                        moves = moves[1..];
                    }
                    else
                    {
                        moves = string.Empty;
                    }
                }

                currentTile = Tile.Move(currentTile, move);
                if (moves.Length == 0)
                {
                    process = false;
                    currentTile.Flip();
                }
            }
        }

        public static Tile Move(Tile currentTile, string move)
        {
            Tile t = move switch
            {
                "ne" => currentTile.MoveNorthEast(),
                "e" => currentTile.MoveEast(),
                "se" => currentTile.MoveSouthEast(),
                "sw" => currentTile.MoveSouthWest(),
                "w" => currentTile.MoveWest(),
                "nw" => currentTile.MoveNorthWest(),
                _ => throw new InvalidOperationException($"Unexpected value {move} in Move() method"),
            };

            return t;
        }

        public Tile CopyLocationAndColor(Dictionary<(int x, int y, int z), Tile> otherTileCollection)
        {
            Tile t = new Tile((X, Y, Z), otherTileCollection)
            {
                Color = Color,
            };

            return t;
        }

        public void Flip()
        {
            Color = Color == 'w' ? 'b' : 'w';
        }

        public int GetNeigborBlackTileCount()
        {
            int blackCount = 0;

            if (MoveEast().Color == 'b')
            {
                blackCount++;
            }

            if (MoveSouthEast().Color == 'b')
            {
                blackCount++;
            }

            if (MoveSouthWest().Color == 'b')
            {
                blackCount++;
            }

            if (MoveWest().Color == 'b')
            {
                blackCount++;
            }

            if (MoveNorthWest().Color == 'b')
            {
                blackCount++;
            }

            if (MoveNorthEast().Color == 'b')
            {
                blackCount++;
            }

            return blackCount;
        }

        public Tile MoveNorthEast()
        {
            int newX = X + 1;
            int newY = Y;
            int newZ = Z - 1;
            var newLocation = (newX, newY, newZ);
            Tile t = AllTiles.ContainsKey(newLocation) ? AllTiles[newLocation] : new Tile(newLocation, AllTiles);

            return t;
        }

        public Tile MoveEast()
        {
            int newX = X + 1;
            int newY = Y - 1;
            int newZ = Z;
            var newLocation = (newX, newY, newZ);
            Tile t = AllTiles.ContainsKey(newLocation) ? AllTiles[newLocation] : new Tile(newLocation, AllTiles);

            return t;
        }

        public Tile MoveSouthEast()
        {
            int newX = X;
            int newY = Y - 1;
            int newZ = Z + 1;
            var newLocation = (newX, newY, newZ);
            Tile t = AllTiles.ContainsKey(newLocation) ? AllTiles[newLocation] : new Tile(newLocation, AllTiles);

            return t;
        }

        public Tile MoveSouthWest()
        {
            int newX = X - 1;
            int newY = Y;
            int newZ = Z + 1;
            var newLocation = (newX, newY, newZ);
            Tile t = AllTiles.ContainsKey(newLocation) ? AllTiles[newLocation] : new Tile(newLocation, AllTiles);

            return t;
        }

        public Tile MoveWest()
        {
            int newX = X - 1;
            int newY = Y + 1;
            int newZ = Z;
            var newLocation = (newX, newY, newZ);
            Tile t = AllTiles.ContainsKey(newLocation) ? AllTiles[newLocation] : new Tile(newLocation, AllTiles);

            return t;
        }

        public Tile MoveNorthWest()
        {
            int newX = X;
            int newY = Y + 1;
            int newZ = Z - 1;
            var newLocation = (newX, newY, newZ);
            Tile t = AllTiles.ContainsKey(newLocation) ? AllTiles[newLocation] : new Tile(newLocation, AllTiles);

            return t;
        }
    }
}