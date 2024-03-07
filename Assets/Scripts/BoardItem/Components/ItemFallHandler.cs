using BlastGame.BoardItems.Strategies.FallStrategies;
using BlastGame.BoardManagement.Tiles;
using BlastGame.FillsAndFallsSystem;
using BlastGame.ServiceManagement;
using BlastGame.StaticDatas;
using UnityEngine;

namespace BlastGame.BoardItems.Components
{
    public class ItemFallHandler : BoardItemComponentBase
    {
        private float _currentSpeed = 0f;

        private Tile _targetTile;

        private bool _shouldFall;

        private ItemFallStrategy _itemFallStrategy;

        public void SetStrategy(ItemFallStrategy itemFallStrategy)
        {
            _itemFallStrategy = itemFallStrategy;
        }

        protected override void Awake()
        {
            base.Awake();
            _attachedItem.OnTileSet += SetTile;
        }

        private void OnEnable()
        {
            ServiceLocator.Instance.GetService<FallMovementController>().RegisterToFallController(this);
        }

        public void SetTile(Tile tile)
        {
            if (tile != _targetTile)
            {
                _targetTile = tile;
                _shouldFall = true;
                _currentSpeed = StaticGameData.FALL_SPEED;

                if (_itemFallStrategy != null)
                {
                    _itemFallStrategy.ExecuteStrategy(_attachedItem);
                }
            }
        }

        public void UpdateMovement()
        {
            if (!_shouldFall)
            {
                return;
            }

            _currentSpeed = Mathf.Min(StaticGameData.MAX_SPEED, _currentSpeed);

            transform.position += _currentSpeed * Time.deltaTime * Vector3.down;

            _currentSpeed += StaticGameData.ACCELERATION;

            if (transform.position.y <= _targetTile.Position.y)
            {
                _shouldFall = false;

                transform.position = _targetTile.Position;

                _attachedItem.Settle();
            }
        }

        private void OnDisable()
        {
            ServiceLocator.Instance.GetService<FallMovementController>().Unregister(this);
        }

        private void OnDestroy()
        {
            _attachedItem.OnTileSet -= SetTile;
        }
    }
}
