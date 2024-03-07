using UnityEngine;

namespace BlastGame.UI.Animations
{
    public abstract class UIAnimationBase : MonoBehaviour
    {
        private void Awake()
        {
            if (TryGetComponent(out IAnimatableUIItem animatableUIItem))
            {
                animatableUIItem.AddListener(PlayAnimation);
            }
        }
        protected abstract void PlayAnimation();
    }
}
