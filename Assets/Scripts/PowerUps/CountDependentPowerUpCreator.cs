using BlastGame.BoardItems.Factory;
using BlastGame.BoardManagement.Tiles;
using BlastGame.ServiceManagement;
using System.Collections.Generic;
using System.Linq;

namespace BlastGame.PowerUpSystem
{
    public class CountDependentPowerUpSelector : IPowerUpSelector
    {
        private List<PowerUpBase> _powerUps;

        public CountDependentPowerUpSelector()
        {
            BoardItemFactory factory = ServiceLocator.Instance.GetService<BoardItemFactory>();

            _powerUps = factory.PowerUps.OrderByDescending(x => x.Cost).ToList();
        }

        public bool TryCreatePowerUp(List<Tile> matchedTiles, out PowerUpBase selectedPowerUp)
        {
            for (int i = 0; i < _powerUps.Count; i++)
            {
                if (_powerUps[i].Cost <= matchedTiles.Count)
                {

                    selectedPowerUp = _powerUps[i];
                    return true;
                }
            }

            selectedPowerUp = null;
            return false;
        }

        public void Dispose()
        {

        }
    }
}
