using BlastGame.BoardManagement.Tiles;
using BlastGame.ServiceManagement;
using System;
using System.Collections.Generic;

namespace BlastGame.MatchFindingSystem
{
    public interface IMatchFinder : IService, IDisposable
    {
        public List<Tile> FindMatches(Tile tile);
    }
}
