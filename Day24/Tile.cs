namespace AOC2020.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Tile
    {
        public Tile(TileManager manager, int tilenumber)
        {
            Manager = manager;
            Tilenumber = tilenumber;
            X = 0;
            Y = 0;
            Z = 0;
        }

        public TileManager Manager { get; init; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public int Tilenumber { get; init; }

        public char Color { get; set; } = 'w';

        public int Ring
        {
            get
            {
                return GetRing(X, Y, Z);
            }
        }

        public static int GetRing(int x, int y, int z)
        {
            int max = -1;
            max = (Math.Abs(x) > max) ? Math.Abs(x) : max;
            max = (Math.Abs(y) > max) ? Math.Abs(y) : max;
            max = (Math.Abs(z) > max) ? Math.Abs(z) : max;
            return max;
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

        public Tile CopyLocationAndColor(TileManager otherTileManager)
        {
            Tile t = new Tile(otherTileManager, Tilenumber)
            {
                Color = Color,
            };

            return t;
        }

        public void Flip()
        {
            if (Ring > Manager.MaxBlackRing)
            {
                Manager.MaxBlackRing = Ring;
            }

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
            Tile t = Manager.GetTile(newX, newY, newZ);

            return t;
        }

        public Tile MoveEast()
        {
            int newX = X + 1;
            int newY = Y - 1;
            int newZ = Z;
            Tile t = Manager.GetTile(newX, newY, newZ);

            return t;
        }

        public Tile MoveSouthEast()
        {
            int newX = X;
            int newY = Y - 1;
            int newZ = Z + 1;
            Tile t = Manager.GetTile(newX, newY, newZ);

            return t;
        }

        public Tile MoveSouthWest()
        {
            int newX = X - 1;
            int newY = Y;
            int newZ = Z + 1;
            Tile t = Manager.GetTile(newX, newY, newZ);

            return t;
        }

        public Tile MoveWest()
        {
            int newX = X - 1;
            int newY = Y + 1;
            int newZ = Z;
            Tile t = Manager.GetTile(newX, newY, newZ);

            return t;
        }

        public Tile MoveNorthWest()
        {
            int newX = X;
            int newY = Y + 1;
            int newZ = Z - 1;
            Tile t = Manager.GetTile(newX, newY, newZ);

            return t;
        }
    }
}