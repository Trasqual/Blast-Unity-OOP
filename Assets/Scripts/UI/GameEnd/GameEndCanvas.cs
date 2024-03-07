using BlastGame.CommandSystem;
using BlastGame.ServiceManagement;
using BlastGame.UI.Animations;
using System;
using UnityEngine;

namespace BlastGame.UI.GameEndManagement
{
    public abstract class GameEndCanvas : MonoBehaviour, ICommand
    {
        [SerializeField] protected CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup.alpha = 0;

            ServiceLocator.Instance.GetService<UIAnimationCommandManager>().RegisterAndProcessCommand(this);
        }

        public abstract void Execute(Action OnComplete);
    }
}
