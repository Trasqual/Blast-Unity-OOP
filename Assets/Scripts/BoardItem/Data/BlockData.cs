using BlastGame.BoardItems.Core;
using BlastGame.BoardItems.Factory;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.BoardItems.Data
{
    [CreateAssetMenu(fileName = "BlockData", menuName = "Data/ItemData/BlockData")]
    public class BlockData : ItemData
    {
        public override BoardItem Build()
        {
            return ServiceLocator.Instance.GetService<BoardItemFactory>().GetBlock(this);
        }
    }
}
