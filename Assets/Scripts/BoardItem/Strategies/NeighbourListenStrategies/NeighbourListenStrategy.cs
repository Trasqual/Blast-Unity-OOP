using BlastGame.BoardItems.Core;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.NeighbourListenStrategies
{
    public abstract class NeighbourListenStrategy : ScriptableObject, IStrategy
    {
        public abstract void ExecuteStrategy(BoardItem boardItem);
    }
}
