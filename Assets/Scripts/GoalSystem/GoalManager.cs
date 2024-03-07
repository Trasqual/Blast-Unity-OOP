using BlastGame.BoardItems.Data;
using BlastGame.EventSystem;
using System.Collections.Generic;

namespace BlastGame.GoalSystem
{
    public class GoalManager : GoalManagerBase
    {
        private GoalProcessor _processor;

        public GoalManager(IGoalProvider goalProvider) : base(goalProvider)
        {
            _processor = new(_goals);
        }

        public override void RegisterGoalItem(ItemCollectionData itemCollectionData)
        {
            if (_goals.Count <= 0)
            {
                return;
            }

            ItemData itemData = itemCollectionData.ItemData;

            if (itemData == null)
            {
                throw new System.Exception("Registered Item Data in GoalManager is null");
            }

            if (GoalExists(itemData))
            {
                EventManager.Instance.TriggerEvent<GoalItemBeingProcessedEvent>(new GoalItemBeingProcessedEvent { ItemCollectionData = itemCollectionData });

                _processor.ProcessGoal(itemData);
            }
        }

        public override bool AllGoalsAreCompleted()
        {
            return _processor.AllGoalsAreCompleted();
        }
    }

    public class GoalProcessor
    {
        private Dictionary<ItemData, int> _goals = new();

        public GoalProcessor(Dictionary<ItemData, int> goals)
        {
            _goals = goals;
        }

        public void ProcessGoal(ItemData data)
        {
            if (_goals.TryGetValue(data, out var count))
            {
                int goalRequiredCount = count;

                if (goalRequiredCount > 0)
                {
                    goalRequiredCount--;


                    if (goalRequiredCount <= 0)
                    {
                        goalRequiredCount = 0;
                    }

                    _goals[data] = goalRequiredCount;

                    if (AllGoalsAreCompleted())
                    {
                        EventManager.Instance.TriggerEvent<AllGoalsAreCompletedEvent>();
                    }
                }
            }
        }

        public bool AllGoalsAreCompleted()
        {
            foreach (var count in _goals.Values)
            {
                if (count > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
