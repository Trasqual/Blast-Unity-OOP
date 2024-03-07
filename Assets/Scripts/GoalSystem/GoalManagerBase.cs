using BlastGame.BoardItems.Data;
using BlastGame.LevelManagement.Datas;
using System.Collections.Generic;

namespace BlastGame.GoalSystem
{
    public abstract class GoalManagerBase : IGoalManager
    {
        protected IGoalProvider _goalProvider;

        protected Dictionary<ItemData, int> _goals = new();

        public GoalManagerBase(IGoalProvider goalProvider)
        {
            _goalProvider = goalProvider;

            InitializeGoals(_goalProvider.GetGoals());
        }

        public bool CanProcessItem(ItemCollectionData itemCollectionData)
        {
            return _goals.Count > 0 && GoalExists(itemCollectionData.ItemData);
        }

        public virtual void InitializeGoals(List<GoalData> goals)
        {
            for (int i = 0; i < goals.Count; i++)
            {
                _goals.Add(goals[i].ItemData, goals[i].RequireToComplete);
            }
        }

        protected bool GoalExists(ItemData data)
        {
            if (_goals.TryGetValue(data, out var count))
            {
                return count > 0;
            }
            return false;
        }

        public abstract void RegisterGoalItem(ItemCollectionData itemCollectionData);

        public abstract bool AllGoalsAreCompleted();

        public void Dispose()
        {

        }
    }
}
