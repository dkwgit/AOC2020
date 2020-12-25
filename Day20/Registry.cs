namespace AOC2020.Day20
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    internal class Registry
    {
        public Dictionary<string, List<(int tileId, int matchingIndex)>> EdgeToTiles { get; } = new ();

        public Dictionary<int, List<(int tileId, string edge)>> TileToNeighborTilesWithEdge { get; } = new ();

        public void RegisterEdge(string edge, int tileId, int matchingIndex)
        {
            if (EdgeToTiles.ContainsKey(edge))
            {
                List<(int tileId, int matchingIndex)> tileList = EdgeToTiles[edge];

                Debug.Assert(!tileList.Any(x => x.tileId == tileId), "Tile should not yet be present");
                Debug.Assert(tileList.Count == 1, "Should only be one prior entry for an edge");

                tileList.Add((tileId, matchingIndex));

                RegisterNeighbors(tileId, tileList[0].tileId, edge);
            }
            else
            {
                EdgeToTiles.Add(edge, new List<(int tileId, int matchingIndex)>() { (tileId, matchingIndex) });
            }
        }

        public void RegisterNeighbors(int tileId, int neighborTileId, string edge)
        {
            if (TileToNeighborTilesWithEdge.ContainsKey(tileId))
            {
                List<(int tileId, string edge)> neighborList = TileToNeighborTilesWithEdge[tileId];
                if (!neighborList.Any(x => x.tileId == neighborTileId && x.edge == edge))
                {
                    neighborList.Add((neighborTileId, edge));
                }
            }
            else
            {
                TileToNeighborTilesWithEdge.Add(tileId, new List<(int tileId, string edge)>() { (neighborTileId, edge) });
            }

            if (TileToNeighborTilesWithEdge.ContainsKey(neighborTileId))
            {
                List<(int tileId, string edge)> neighborList = TileToNeighborTilesWithEdge[neighborTileId];
                if (!neighborList.Any(x => x.tileId == tileId && x.edge == edge))
                {
                    neighborList.Add((tileId, edge));
                }
            }
            else
            {
                TileToNeighborTilesWithEdge.Add(neighborTileId, new List<(int tileId, string edge)>() { (tileId, edge) });
            }
        }
    }
}
