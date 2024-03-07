using BlastGame.BoardManagement.Tiles;
using BlastGame.ServiceManagement;
using System;
using System.Collections.Generic;

namespace BlastGame.MatchManagement
{
    public interface IMatchManager : IService, IDisposable
    {
        public void ProcessMatchedTiles(Tile clickedTile, List<Tile> matchedTiles);
    }
}
