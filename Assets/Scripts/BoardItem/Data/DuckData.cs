using BlastGame.BoardItems.Core;
using BlastGame.BoardItems.Factory;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.BoardItems.Data
{
    [CreateAssetMenu(fileName = "DuckData", menuName = "Data/ItemData/DuckData")]
    public class DuckData : ItemData
    {
        public override BoardItem Build()
        {
            return ServiceLocator.Instance.GetService<BoardItemFactory>().GetDuck();
        }
    }
}
