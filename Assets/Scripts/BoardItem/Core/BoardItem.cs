using BlastGame.BoardItems.Components;
using BlastGame.BoardItems.Data;
using BlastGame.BoardManagement.Tiles;
using BlastGame.ObjectPoolingSystem;
using BlastGame.PowerUpSystem;
using BlastGame.ServiceManagement;
using System;
using UnityEngine;

namespace BlastGame.BoardItems.Core
{
    public class BoardItem : MonoBehaviour
    {
        public event Action<Tile> OnTileSet;
        public event Action<ItemData> OnItemDataSet;
        public event Action OnItemSettled;

        public Tile CurrentTile { get; private set; }

        public ItemColor Color => _itemData ? _itemData.Color : ItemColor.None;

        private ItemData _itemData;

        public void SetData(ItemData itemData)
        {
            _itemData = itemData;
            OnItemDataSet?.Invoke(_itemData);
        }

        public ItemData GetData()
        {
            return _itemData;
        }

        public void SetTileAndPosition(Tile tile)
        {
            SetTile(tile);
            SetPosition(tile.Position);
        }

        public void SetTile(Tile tile)
        {
            if (CurrentTile == tile)
            {
                return;
            }

            ReleaseTile();

            CurrentTile = tile;
            CurrentTile.SetItem(this);
            CurrentTile.ChangeState(new TileRecievingState());

            OnTileSet?.Invoke(tile);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
            Settle();
        }

        public void Settle()
        {
            if (CurrentTile != null)
            {
                CurrentTile.ChangeState(new TileSettledState());
                OnItemSettled?.Invoke();
            }
        }

        public void ClearAndReleaseToPool()
        {
            Component[] components = GetComponents(typeof(BoardItemComponentBase));

            foreach (Component component in components)
            {
                if (component is not ItemSpriteHandler)
                {
                    Destroy(component);
                }
            }

            PowerUpBase powerUp = GetComponentInChildren<PowerUpBase>();
            if (powerUp != null)
            {
                powerUp.transform.SetParent(null);
            }

            _itemData = null;
            CurrentTile = null;

            ServiceLocator.Instance.GetService<IObjectPoolManager>().ReleaseObject(this);
        }

        public void ReleaseTile()
        {
            if (CurrentTile != null)
            {
                CurrentTile.SetItem(null);
                CurrentTile.ChangeState(new TileEmptyState());
                CurrentTile = null;
            }
        }
    }
}
