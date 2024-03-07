using BlastGame.BoardItems.Core;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.NeighbourListenStrategies
{
    [CreateAssetMenu(fileName = "PopWithNeighbourListenStrategy", menuName = "Strategies/NeighbourListenStrategy/PopWithNeighbourListenStrategy")]
    public class PopWithNeighbourListenStrategy : NeighbourListenStrategy
    {
        public override void ExecuteStrategy(BoardItem boardItem)
        {
            boardItem.CurrentTile?.Pop(this);
        }
    }
}
