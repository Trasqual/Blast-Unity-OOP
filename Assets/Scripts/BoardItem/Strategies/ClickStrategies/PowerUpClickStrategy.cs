using BlastGame.BoardItems.Core;
using BlastGame.MatchManagement;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.ClickStrategies
{
    [CreateAssetMenu(fileName = "PowerUpClickStrategy", menuName = "Strategies/ItemClickStrategy/PowerUpClickStrategy")]
    public class PowerUpClickStrategy : ItemClickStrategy
    {
        public override void Execute(BoardItem item)
        {
            ServiceLocator.Instance.GetService<IMatchManager>().ProcessMatchedTiles(item.CurrentTile, null);
        }
    }
}
