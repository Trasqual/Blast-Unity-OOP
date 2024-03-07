using BlastGame.BoardItems.Core;
using BlastGame.BoardManagement;
using BlastGame.BoardManagement.Tiles;
using BlastGame.FillsAndFallsSystem.SpawnSystem;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.FillsAndFallsSystem
{
    public class FillAndFallManager : MonoBehaviour
    {
        private BoardBase _board;

        private FallTargetProvider _targetProvider;

        private IItemSpawner _itemSpawner;

        private void Start()
        {
            _board = ServiceLocator.Instance.GetService<BoardBase>();

            _itemSpawner = new RandomBlockSpawner();
            _targetProvider = new FallTargetProvider();
        }

        private void FixedUpdate()
        {
            DoFills();
            DoFalls();
        }

        private void DoFills()
        {
            for (int i = 0; i < _board.Size.x; i++)
            {
                for (int j = 0; j < _board.Size.y; j++)
                {
                    Tile currentTile = _board.GetTile(i, j);
                    if (currentTile.State is not TileSettledState)
                    {
                        continue;
                    }

                    BoardItem item = currentTile.GetItem();

                    if (item != null)
                    {
                        Tile fallTarget = _targetProvider.GetFallTarget(currentTile.X, currentTile.Y);

                        item.SetTile(fallTarget);
                    }
                }
            }
        }

        private void DoFalls()
        {
            for (int i = 0; i < _board.Size.x; i++)
            {
                if (_board.GetTile(i, _board.Size.y - 1) != null && _board.GetTile(i, _board.Size.y - 1).State is TileEmptyState)
                {
                    BoardItem spawn = _itemSpawner.GetItem();

                    Tile targetTile = _targetProvider.GetFallTarget(i, _board.Size.y);
                    spawn.SetTile(targetTile);

                    Vector3 spawnPosition = _board.GetAlignedPosition(i, _board.Size.y + 1);

                    float yOffset = 0f;

                    if (targetTile.Down != null && targetTile.Down.GetItem() != null)
                    {
                        yOffset = targetTile.Down.GetItem().transform.position.y + _board.CellSize;
                    }

                    spawnPosition.y = spawnPosition.y > yOffset ? spawnPosition.y : yOffset;

                    spawn.SetPosition(spawnPosition);
                }
            }
        }
    }
}
