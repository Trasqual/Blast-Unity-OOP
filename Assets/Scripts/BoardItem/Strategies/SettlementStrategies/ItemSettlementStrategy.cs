using BlastGame.BoardItems.Core;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.SettlementStrategies
{
    public abstract class ItemSettlementStrategy : ScriptableObject, IStrategy
    {
        public abstract void ExecuteStrategy(BoardItem item);
    }
}
