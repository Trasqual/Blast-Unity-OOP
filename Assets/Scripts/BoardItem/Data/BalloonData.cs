using BlastGame.BoardItems.Core;
using BlastGame.BoardItems.Factory;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.BoardItems.Data
{
    [CreateAssetMenu(fileName = "BalloonData", menuName = "Data/ItemData/BalloonData")]
    public class BalloonData : ItemData
    {
        public override BoardItem Build()
        {
            return ServiceLocator.Instance.GetService<BoardItemFactory>().GetBalloon(this);
        }
    }
}
