using BlastGame.LevelManagement.Datas;
using BlastGame.ServiceManagement;

namespace BlastGame.LevelManagement
{
    public interface ILevelManager : IService
    {
        public LevelData GetLevelData();
    }
}
