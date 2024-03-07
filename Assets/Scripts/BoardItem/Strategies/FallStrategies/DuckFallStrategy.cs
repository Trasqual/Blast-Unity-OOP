using BlastGame.BoardItems.Core;
using BlastGame.EventSystem;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.FallStrategies
{
    [CreateAssetMenu(fileName = "DuckFallStrategy", menuName = "Strategies/ItemFallStrategy/DuckFallStrategy")]
    public class DuckFallStrategy : ItemFallStrategy
    {
        public override void ExecuteStrategy(BoardItem item)
        {
            if (item.CurrentTile.Y == 0)
            {
                EventManager.Instance.TriggerEvent<BlockGameLoopEvent>();
            }
        }
    }
}
