using BlastGame.BoardItems.Core;

namespace BlastGame.FillsAndFallsSystem.SpawnSystem
{
    public interface IItemSpawner
    {
        public BoardItem GetItem();
    }
}
