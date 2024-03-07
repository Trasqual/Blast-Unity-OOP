using BlastGame.BoardItems.Data;
using UnityEngine;

namespace BlastGame.BoardItems.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemSpriteHandler : BoardItemComponentBase
    {
        private SpriteRenderer _spriteRenderer;

        private ItemData _data;

        protected override void Awake()
        {
            base.Awake();

            _spriteRenderer = GetComponent<SpriteRenderer>();

            _attachedItem.OnItemDataSet += SetData;
            _attachedItem.OnItemSettled += SetSpriteOrder;
        }

        private void SetData(ItemData data)
        {
            _data = data;
            UpdateSprite(0);
        }

        private void UpdateSprite(int spriteId)
        {
            if (_data == null || _data.Sprites.Count <= 0)
            {
                return;
            }
            _spriteRenderer.sprite = _data.Sprites[spriteId];
        }

        private void SetSpriteOrder()
        {
            if (_attachedItem.CurrentTile != null)
            {
                _spriteRenderer.sortingOrder = _attachedItem.CurrentTile.X + _attachedItem.CurrentTile.Y;
            }
            else
            {
                _spriteRenderer.sortingOrder = Mathf.RoundToInt(Mathf.Abs(_attachedItem.transform.position.x) + Mathf.Abs(_attachedItem.transform.position.y));
            }
        }

        private void OnDisable()
        {
            _spriteRenderer.sprite = null;
        }

        private void OnDestroy()
        {
            _attachedItem.OnItemDataSet -= SetData;
            _attachedItem.OnItemSettled -= SetSpriteOrder;
        }
    }
}
