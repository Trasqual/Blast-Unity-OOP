using BlastGame.LevelManagement.Datas;
using BlastGame.ServiceManagement;
using System;
using System.Collections.Generic;

namespace BlastGame.GoalSystem
{
    public interface IGoalManager : IService, IDisposable
    {
        public void InitializeGoals(List<GoalData> goals);
        public bool CanProcessItem(ItemCollectionData itemCollectionData);
        public void RegisterGoalItem(ItemCollectionData itemCollectionData);
        public bool AllGoalsAreCompleted();
    }
}
