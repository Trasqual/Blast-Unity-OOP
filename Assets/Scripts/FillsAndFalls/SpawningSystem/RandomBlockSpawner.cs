using BlastGame.BoardItems.Core;
using BlastGame.BoardItems.Factory;
using BlastGame.ServiceManagement;

namespace BlastGame.FillsAndFallsSystem.SpawnSystem
{
    public class RandomBlockSpawner : IItemSpawner
    {
        private BoardItemFactory _itemFactory;

        public RandomBlockSpawner()
        {
            _itemFactory = ServiceLocator.Instance.GetService<BoardItemFactory>();
        }

        public BoardItem GetItem()
        {
            return _itemFactory.GetRandomBlock();
        }
    }
}
