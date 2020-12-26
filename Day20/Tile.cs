namespace AOC2020.Day20
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    internal class Tile
    {
        private int _matchingIndex = -1;

        private char[,] _finalOrientation;

        public Tile(Registry registry, int id, int length, char[,] initialData) => (Registry, Id, Length, Data, Edges) = (registry, id, length, Tile.SetupData(length, initialData, out List<List<string>> edges), Tile.RegisterEdges(registry, id, edges));

        public enum EdgeEnum
        {
#pragma warning disable SA1602 // Enumeration items should be documented
            Top = 0,
            Right = 1,
            Bottom = 2,
            Left = 3,
            Invalid = -1,
#pragma warning restore SA1602 // Enumeration items should be documented
        }

        public Registry Registry { get; init; }

        public int Id { get; init; }

        public int Length { get; init; }

        public List<char[,]> Data { get; init; }

        public List<List<string>> Edges { get; init; }

        public char[,] FinalOrientation
        {
            get
            {
                return _finalOrientation;
            }

            set
            {
                _finalOrientation = value;
            }
        }

        public int MatchingDataIndex
        {
            get
            {
                return _matchingIndex;
            }

            set
            {
                Debug.Assert(_matchingIndex == -1, "Expecting only 1 set ever");
                _matchingIndex = value;
            }
        }

        public static List<char[,]> SetupData(int length, char[,] initialData, out List<List<string>> edges)
        {
            List<char[,]> data = new () { initialData, Tile.FlipAlongX(length, initialData) };

            edges = new ();

            for (int i = 0; i < 2; i++)
            {
                StringBuilder topRow = new ();
                StringBuilder bottomRow = new ();
                StringBuilder leftColumn = new ();
                StringBuilder rightColumn = new ();

                for (int column = 0; column < length; column++)
                {
                    topRow.Append(data[i][0, column]);
                    bottomRow.Append(data[i][length - 1, length - 1 - column]);
                }

                for (int row = 0; row < length; row++)
                {
                    rightColumn.Append(data[i][row, length - 1]);
                    leftColumn.Append(data[i][length - 1 - row, 0]);
                }

                edges.Add(new List<string>() { topRow.ToString(), rightColumn.ToString(), bottomRow.ToString(), leftColumn.ToString() });
            }

            return data;
        }

        public static List<List<string>> RegisterEdges(Registry registry, int id, List<List<string>> edges)
        {
            Debug.Assert(edges.Count == 2, "Expecting only two edge lists within edges");
            Debug.Assert(edges.All(x => x.Count == 4), "Each list of edges within edges should have four edges");

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < edges[i].Count; j++)
                {
                    registry.RegisterEdge(edges[i][j], id, i);
                }
            }

            return edges;
        }

        public static char[,] FlipAlongX(int length, char[,] initialData)
        {
            char[,] flipped = new char[length, length];

            for (int row = 0; row < length; row++)
            {
                for (int column = 0; column < length; column++)
                {
                    flipped[length - 1 - row, column] = initialData[row, column];
                }
            }

            return flipped;
        }

        public static char[,] RotateRight(char[,] data, int numberOfRotations, int length)
        {
            for (int rotations = 0; rotations < numberOfRotations; rotations++)
            {
                data = Tile.RotateRightOnce(data, length);
            }

            return data;
        }

        public static char[,] RotateRightOnce(char[,] data, int length)
        {
            // PrintTile(GetTileText(data, length));
            char[,] newData = new char[length, length];

            for (int row = 0; row < length; row++)
            {
                for (int column = 0; column < length; column++)
                {
                    newData[column, length - 1 - row] = data[row, column];
                }
            }

            // PrintTile(GetTileText(newData, length));
            return newData;
        }

        public static List<string> GetTileText(char[,] data, int length)
        {
            List<string> tileText = new ();

            for (int row = 0; row < length; row++)
            {
                StringBuilder sb = new ();
                for (int column = 0; column < length; column++)
                {
                    sb.Append(data[row, column]);
                }

                tileText.Add(sb.ToString());
            }

            return tileText;
        }

        public static void PrintTile(List<string> tileRows)
        {
            foreach (var row in tileRows)
            {
                Console.WriteLine(row);
            }

            Console.WriteLine(string.Empty);
        }

        public EdgeEnum MatchEdge(string edge, int matchingIndex)
        {
            char[,] data = Data[matchingIndex];

            if (MatchTopEdge(edge, data))
            {
                return EdgeEnum.Top;
            }

            if (MatchRightEdge(edge, data))
            {
                return EdgeEnum.Right;
            }

            if (MatchBottomEdge(edge, data))
            {
                return EdgeEnum.Bottom;
            }

            if (MatchLeftEdge(edge, data))
            {
                return EdgeEnum.Left;
            }

            return EdgeEnum.Invalid;
        }

        public string GetEdge(Tile.EdgeEnum edgeToGet)
        {
            string edge = edgeToGet switch
            {
                Tile.EdgeEnum.Top => GetTopEdge(),
                Tile.EdgeEnum.Right => GetRightEdge(),
                Tile.EdgeEnum.Bottom => GetBottomEdge(),
                Tile.EdgeEnum.Left => GetLeftEdge(),
                _ => throw new InvalidOperationException("Unexpected value for EdgeEnum in GetEdge()"),
            };

            return edge;
        }

        private string GetTopEdge()
        {
            StringBuilder sb = new ();
            for (int column = 0; column < Length; column++)
            {
                sb.Append(FinalOrientation[0, column]);
            }

            return sb.ToString();
        }

        private string GetRightEdge()
        {
            StringBuilder sb = new ();
            for (int row = 0; row < Length; row++)
            {
                sb.Append(FinalOrientation[row, Length - 1]);
            }

            return sb.ToString();
        }

        private string GetBottomEdge()
        {
            StringBuilder sb = new ();
            for (int column = Length - 1; column >= 0; column--)
            {
                sb.Append(FinalOrientation[Length - 1, column]);
            }

            return sb.ToString();
        }

        private string GetLeftEdge()
        {
            StringBuilder sb = new ();
            for (int row = Length - 1; row >= 0; row--)
            {
                sb.Append(FinalOrientation[row, 0]);
            }

            return sb.ToString();
        }

        private bool MatchTopEdge(string edge, char[,] data)
        {
            for (int column = 0; column < Length; column++)
            {
                if (data[0, column] != edge[column])
                {
                    return false;
                }
            }

            return true;
        }

        private bool MatchRightEdge(string edge, char[,] data)
        {
            for (int row = 0; row < Length; row++)
            {
                if (data[row, Length - 1] != edge[row])
                {
                    return false;
                }
            }

            return true;
        }

        private bool MatchBottomEdge(string edge, char[,] data)
        {
            for (int column = 0; column < Length; column++)
            {
                if (data[Length - 1, Length - 1 - column] != edge[column])
                {
                    return false;
                }
            }

            return true;
        }

        private bool MatchLeftEdge(string edge, char[,] data)
        {
            for (int row = 0; row < Length; row++)
            {
                if (data[Length - 1 - row, 0] != edge[row])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
