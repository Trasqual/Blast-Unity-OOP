using BlastGame.BoardManagement;
using BlastGame.BoardManagement.Tiles;
using System.Collections.Generic;

namespace BlastGame.MatchFindingSystem
{
    public class FlowFieldMatchFinder : IMatchFinder
    {
        private BoardBase _board;

        private Queue<Tile> _tileQueue;

        private List<Tile> _processedTiles;
        private List<Tile> _matchingTiles;

        public FlowFieldMatchFinder(BoardBase board)
        {
            _board = board;

            _tileQueue = new Queue<Tile>();
            _processedTiles = new List<Tile>(_board.ItemCount);
            _matchingTiles = new List<Tile>();
        }

        public List<Tile> FindMatches(Tile tile)
        {
            _processedTiles.Clear();
            _matchingTiles.Clear();

            _tileQueue.Enqueue(tile);
            _processedTiles.Add(tile);
            _matchingTiles.Add(tile);

            while (_tileQueue.Count > 0)
            {
                Tile testedTile = _tileQueue.Dequeue();

                foreach (var neighbour in testedTile.Neighbours.Values)
                {
                    if (neighbour.State is TileSettledState && !_processedTiles.Contains(neighbour) && neighbour.GetItem().Color == tile.GetItem().Color)
                    {
                        _processedTiles.Add(neighbour);
                        _matchingTiles.Add(neighbour);
                        _tileQueue.Enqueue(neighbour);
                    }
                }
            }

            return _matchingTiles;
        }

        public void Dispose()
        {

        }
    }
}
