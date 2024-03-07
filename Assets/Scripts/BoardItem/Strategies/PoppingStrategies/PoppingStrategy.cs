using BlastGame.BoardItems.Core;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.PoppingStrategies
{
    public abstract class PoppingStrategy : ScriptableObject, IStrategy
    {
        public abstract void ExecuteStrategy(BoardItem item);

        public abstract bool CanBePopped(object poppedBy);
    }
}
