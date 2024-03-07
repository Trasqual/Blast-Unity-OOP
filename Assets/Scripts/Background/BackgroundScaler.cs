using BlastGame.EventSystem;
using UnityEngine;

namespace BlastGame.GameBackground
{
    [DefaultExecutionOrder(-10)]
    public class BackgroundScaler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            EventManager.Instance.AddListener<CameraIsScaledEvent>(OnCameraIsScaled);
        }

        private void OnCameraIsScaled(object eventData)
        {
            Camera cam = ((CameraIsScaledEvent)eventData).Cam;

            float spriteWidth = _spriteRenderer.bounds.size.x;
            float spriteHeight = _spriteRenderer.bounds.size.y;

            float camHeight = cam.orthographicSize * 2f;
            float camWidth = camHeight * cam.aspect;

            float scaleHeight = camHeight / spriteHeight;
            float scaleWidth = camWidth / spriteWidth;

            if (scaleHeight > scaleWidth)
            {
                transform.localScale = new Vector3(scaleHeight, scaleHeight, 1f);
            }
            else
            {
                transform.localScale = new Vector3(scaleWidth, scaleWidth, 1f);
            }
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<CameraIsScaledEvent>(OnCameraIsScaled);
        }
    }
}
