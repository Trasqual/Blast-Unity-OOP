using BlastGame.BoardItems.Data;
using System;

namespace BlastGame.LevelManagement.Datas
{
    [Serializable]
    public struct GoalData
    {
        public ItemData ItemData;
        public int RequireToComplete;
    }
}
