using BlastGame.BoardItems.Data;
using BlastGame.BoardManagement;
using BlastGame.GoalSystem;
using BlastGame.MoveManagement;
using BlastGame.ServiceManagement;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.LevelManagement.Datas
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Data/Level Data")]
    public class LevelData : ScriptableObject, IService, IGoalProvider, IMoveProvider
    {
        public BoardData BoardData;

        [Space(10)]
        public List<ItemData> PossibleItems;

        [Space(10)]
        [SerializeField] private List<GoalData> _goals = new();

        [Space(10)]
        [SerializeField] private int _moves;

        public ItemData[] GridData;

        public List<GoalData> GetGoals() => _goals;

        public int GetMoves() => _moves;
    }
}
