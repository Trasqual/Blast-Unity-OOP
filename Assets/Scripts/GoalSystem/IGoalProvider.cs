using BlastGame.LevelManagement.Datas;
using BlastGame.ServiceManagement;
using System.Collections.Generic;

namespace BlastGame.GoalSystem
{
    public interface IGoalProvider : IService
    {
        public List<GoalData> GetGoals();
    }
}
