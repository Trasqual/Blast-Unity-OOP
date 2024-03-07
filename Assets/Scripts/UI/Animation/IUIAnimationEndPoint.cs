using UnityEngine;

namespace BlastGame.UI.Animations
{
    public interface IUIAnimationEndPoint
    {
        public void OnReached();
        public Vector3 GetPosition();
    }
}
