using BlastGame.BoardItems.Core;
using BlastGame.BoardManagement.Tiles;
using BlastGame.EventSystem;
using BlastGame.PowerUpSystem;
using BlastGame.StaticDatas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlastGame.MatchManagement
{
    public class PowerUpOrPopMatchManager : IMatchManager
    {
        private readonly IPowerUpSelector _powerUpSelector;

        public PowerUpOrPopMatchManager(IPowerUpSelector powerUpSelector)
        {
            _powerUpSelector = powerUpSelector;
        }

        public async void ProcessMatchedTiles(Tile clickedTile, List<Tile> matchedTiles)
        {
            if (clickedTile != null && matchedTiles == null)
            {
                EventManager.Instance.TriggerEvent<LegalMoveMadeEvent>();
                clickedTile.Pop(this);
                return;
            }

            if (matchedTiles.Count < StaticGameData.MIN_MATCH_COUNT)
            {
                return;
            }

            foreach (var matchedTile in matchedTiles)
            {
                matchedTile.ChangeState(new TilePoppingOrMergingState());
            }

            EventManager.Instance.TriggerEvent<LegalMoveMadeEvent>();

            if (_powerUpSelector.TryCreatePowerUp(matchedTiles, out PowerUpBase powerUp))
            {
                await MergeMatchedTiles(clickedTile, matchedTiles);

                BoardItem item = powerUp.Build();
                item.SetTileAndPosition(clickedTile);
            }
            else
            {
                foreach (Tile tile in matchedTiles)
                {
                    tile.Pop(this);
                }
            }
        }

        private async Task MergeMatchedTiles(Tile clickedTile, List<Tile> matchedTiles)
        {
            List<Task> moveTasks = new List<Task>();

            foreach (var tile in matchedTiles)
            {
                moveTasks.Add(tile.Merge(this, clickedTile.Position));
            }

            await Task.WhenAll(moveTasks);
        }

        public void Dispose()
        {

        }
    }
}
