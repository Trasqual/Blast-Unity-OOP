using BlastGame.BoardManagement;
using BlastGame.BoardManagement.Tiles;
using BlastGame.ServiceManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.PowerUpSystem
{
    public class RocketMovingPart : MonoBehaviour, IPowerUp
    {
        public event Action OnMovementCompleted;

        [SerializeField] private float _speed;

        private BoardBase _board;

        private Camera _cam;

        private List<Tile> _tilesOnPath = new();

        private void Awake()
        {
            _cam = ServiceLocator.Instance.BoardCamera;
            _board = ServiceLocator.Instance.GetService<BoardBase>();
        }

        public void Activate(Tile startTile)
        {
            LockTilesOnPath(startTile);
            StartCoroutine(MoveCoroutine(startTile));
        }

        private IEnumerator MoveCoroutine(Tile startTile)
        {

            Vector2Int gridPos = _board.GetGridFromWorldPosition(transform.position);

            Tile tile = _board.GetTile(gridPos.x, gridPos.y);

            while (IsInScreen(transform.position))
            {
                transform.position += transform.forward * _speed * Time.deltaTime;

                gridPos = _board.GetGridFromWorldPosition(transform.position);

                tile = _board.GetTile(gridPos.x, gridPos.y);

                if (tile != null && tile.State is TileIsHeldByRocketState)
                {
                    tile.Pop(this);
                }

                yield return null;
            }

            OnMovementCompleted?.Invoke();
        }

        private void LockTilesOnPath(Tile startTile)
        {
            _tilesOnPath.Clear();

            Vector3 ghostPosition = transform.position;

            Vector2Int gridPos = _board.GetGridFromWorldPosition(ghostPosition);

            Tile tile = _board.GetTile(gridPos.x, gridPos.y);

            while (IsInScreen(ghostPosition))
            {
                ghostPosition += transform.forward;

                gridPos = _board.GetGridFromWorldPosition(ghostPosition);

                tile = _board.GetTile(gridPos.x, gridPos.y);

                if (tile != null && tile != startTile && tile.State is TileSettledState)
                {
                    tile.ChangeState(new TileIsHeldByRocketState());
                    _tilesOnPath.Add(tile);
                }
            }
        }

        private bool IsInScreen(Vector3 position)
        {
            Vector3 screenPoint = _cam.WorldToViewportPoint(position);
            return screenPoint.x >= 0f && screenPoint.y >= 0f && screenPoint.x <= 1 && screenPoint.y <= 1;
        }
    }
}
