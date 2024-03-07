using BlastGame.BoardItems.Core;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.ClickStrategies
{
    public abstract class ItemClickStrategy : ScriptableObject
    {
        public abstract void Execute(BoardItem item);
    }
}
