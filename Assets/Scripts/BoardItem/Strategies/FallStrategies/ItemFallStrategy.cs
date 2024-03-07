using BlastGame.BoardItems.Core;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.FallStrategies
{
    public abstract class ItemFallStrategy : ScriptableObject, IStrategy
    {
        public abstract void ExecuteStrategy(BoardItem item);
    }
}
