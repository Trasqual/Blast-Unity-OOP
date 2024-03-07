using BlastGame.LevelManagement.Datas;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.LevelManagement
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        [SerializeField] private LevelData _levelData;

        public LevelData GetLevelData() => _levelData;
    }
}
