using BlastGame.BoardManagement;
using BlastGame.BoardManagement.Tiles;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.FillsAndFallsSystem
{
    public class FallTargetProvider
    {
        private BoardBase _board;

        public FallTargetProvider()
        {
            _board = ServiceLocator.Instance.GetService<BoardBase>();
        }

        public Tile GetFallTarget(int x, int y)
        {
            if (y <= 0)
            {
                return _board.GetTile(x, y);
            }

            y = Mathf.Min(_board.Size.y - 1, y);

            Tile targetTile = _board.GetTile(x, y);

            for (int i = y - 1; i >= 0; i--)
            {
                Tile testedTile = _board.GetTile(x, i);

                if (testedTile.State is TileEmptyState)
                {
                    targetTile = testedTile;
                }
                else
                {
                    break;
                }
            }

            return targetTile;
        }
    }
}
