namespace AOC2020.Day20
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    internal class Tile
    {
        private int _matchingIndex = -1;

        public Tile(Registry registry, int id, int length, char[,] initialData) => (Registry, Id, Length, Data, Edges) = (registry, id, length, Tile.SetupData(length, initialData, out List<List<string>> edges), Tile.RegisterEdges(registry, id, edges));

        public Registry Registry { get; init; }

        public int Id { get; init; }

        public int Length { get; init; }

        public List<char[,]> Data { get; init; }

        public List<List<string>> Edges { get; init; }

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
            List<char[,]> data = new () { initialData, Tile.FlipTileDataAlongX(length, initialData) };

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

        public static char[,] FlipTileDataAlongX(int length, char[,] initialData)
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
    }
}
