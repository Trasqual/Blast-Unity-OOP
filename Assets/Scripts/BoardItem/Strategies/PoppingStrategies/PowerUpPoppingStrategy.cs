using BlastGame.BoardItems.Core;
using BlastGame.PowerUpSystem;
using UnityEngine;

namespace BlastGame.BoardItems.Strategies.PoppingStrategies
{
    [CreateAssetMenu(fileName = "PowerUpPoppingStrategy", menuName = "Strategies/PoppingStrategy/PowerUpPoppingStrategy")]
    public class PowerUpPoppingStrategy : PoppingStrategy
    {
        public override bool CanBePopped(object poppedBy)
        {
            return true;
        }

        public override void ExecuteStrategy(BoardItem item)
        {
            PowerUpBase powerUp = item.GetComponentInChildren<PowerUpBase>();

            if (powerUp != null)
            {
                powerUp.Activate(item.CurrentTile);
            }

            item.ClearAndReleaseToPool();
        }
    }
}
