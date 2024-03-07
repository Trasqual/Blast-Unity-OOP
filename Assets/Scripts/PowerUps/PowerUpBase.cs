using BlastGame.BoardItems.Core;
using BlastGame.BoardManagement.Tiles;
using UnityEngine;

namespace BlastGame.PowerUpSystem
{
    public abstract class PowerUpBase : MonoBehaviour, IPowerUp
    {
        public int Cost;
        public abstract void Activate(Tile startTile);
        public abstract BoardItem Build();
    }
}
