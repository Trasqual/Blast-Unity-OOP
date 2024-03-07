using BlastGame.BoardItems.Components;
using BlastGame.BoardItems.Core;
using BlastGame.EventSystem;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.BoardManagement.Tiles
{
    public class TileClickManager : MonoBehaviour
    {
        private BoardBase _board;

        private void Start()
        {
            _board = ServiceLocator.Instance.GetService<BoardBase>();

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventManager.Instance.AddListener<ClickEvent>(OnClickDown);
        }

        private void OnClickDown(object eventData)
        {
            Vector3 position = ((ClickEvent)eventData).Position;

            Tile clickedTile = _board.GetTileFromPosition(position);

            if (clickedTile == null || clickedTile.GetItem() == null || clickedTile.State is not TileSettledState)
            {
                return;
            }

            BoardItem item = clickedTile.GetItem();

            if (item.TryGetComponent(out ItemClickHandler clickHandler))
            {
                clickHandler.ExecuteClick();
            }
        }

        private void UnsubscribeToEvents()
        {
            EventManager.Instance.RemoveListener<ClickEvent>(OnClickDown);
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
