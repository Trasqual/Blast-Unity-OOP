using BlastGame.BoardItems.Core;
using BlastGame.BoardItems.Factory;
using BlastGame.BoardManagement;
using BlastGame.BoardManagement.Tiles;
using BlastGame.ServiceManagement;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.PowerUpSystem
{
    public class BombPowerUp : PowerUpBase
    {
        private List<Vector2Int> _explodeTile = new()
        {
            new Vector2Int(0,0),
            new Vector2Int(1,0),
            new Vector2Int(-1,0),
            new Vector2Int(0,-1),
            new Vector2Int(0,1),
            new Vector2Int(1,1),
            new Vector2Int(-1,1),
            new Vector2Int(1,-1),
            new Vector2Int(-1,-1),

        };

        public override void Activate(Tile startTile)
        {
            BoardBase board = ServiceLocator.Instance.GetService<BoardBase>();

            for(int i = 0;  i < _explodeTile.Count; i++)
            {
                Tile explodingTile = board.GetTile(startTile.X + _explodeTile[i].x, startTile.Y + _explodeTile[i].y);
                explodingTile.Pop(this);
            }
        }

        public override BoardItem Build()
        {
            return ServiceLocator.Instance.GetService<BoardItemFactory>().GetBomb();
        }
    }
}
