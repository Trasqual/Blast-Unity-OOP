using BlastGame.EventSystem;
using UnityEngine;

namespace BlastGame.BoardManagement.View
{
    [DefaultExecutionOrder(-10)]
    public class BoardBackgroundResizer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            EventManager.Instance.AddListener<BoardIsReadyEvent>(SetBackgroundSize);
        }

        private void SetBackgroundSize(object eventData)
        {
            CentralizedVerticalBoard board = ((BoardIsReadyEvent)eventData).Board;

            float xSize = board.Size.x + board.CellSize * 0.25f;
            float ySize = board.Size.y + board.CellSize * 0.5f;

            _spriteRenderer.size = new Vector2(xSize, ySize);
            transform.position = board.Center;
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<BoardIsReadyEvent>(SetBackgroundSize);
        }
    }
}
