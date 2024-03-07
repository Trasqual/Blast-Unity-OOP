using BlastGame.BoardItems.Core;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.SettlementStrategies
{
    [CreateAssetMenu(fileName = "PopAtBottomSettlementStrategy", menuName = "Strategies/ItemSettlementStrategy/PopAtBottomSettlementStrategy")]
    public class PopAtBottomSettlementStrategy : ItemSettlementStrategy
    {
        public override void ExecuteStrategy(BoardItem item)
        {
            if (item == null || item.CurrentTile == null)
            {
                return;
            }

            if (item.CurrentTile.Y == 0)
            {
                item.CurrentTile.Pop(this);
            }
        }
    }
}
