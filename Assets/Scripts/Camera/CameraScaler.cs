using BlastGame.BoardManagement;
using BlastGame.EventSystem;
using UnityEngine;

namespace BlastGame.CameraManagement
{
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private float _minOrthoSize = 5;

        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;

            EventManager.Instance.AddListener<BoardIsReadyEvent>(OnBoardIsReady);
        }

        private void OnBoardIsReady(object eventData)
        {
            const float VERTICAL_PADDING = 3f;
            const float HORIZONTAL_PADDING = 1f;

            CentralizedVerticalBoard board = ((BoardIsReadyEvent)eventData).Board;

            float scaleHeight = board.Size.y * 0.5f + VERTICAL_PADDING;

            float scaleWidth = board.Size.x / (2f * _cam.aspect) + HORIZONTAL_PADDING;

            float orthoSize = Mathf.Max(scaleHeight, scaleWidth);

            _cam.orthographicSize = Mathf.Max(_minOrthoSize, orthoSize);
            EventManager.Instance.TriggerEvent<CameraIsScaledEvent>(new CameraIsScaledEvent { Cam = _cam });
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<BoardIsReadyEvent>(OnBoardIsReady);
        }
    }
}
