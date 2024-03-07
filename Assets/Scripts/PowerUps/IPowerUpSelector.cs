using BlastGame.BoardManagement.Tiles;
using BlastGame.ServiceManagement;
using System;
using System.Collections.Generic;

namespace BlastGame.PowerUpSystem
{
    public interface IPowerUpSelector : IService, IDisposable
    {
        public bool TryCreatePowerUp(List<Tile> matchedTiles, out PowerUpBase selectedPowerUp);
    }
}
