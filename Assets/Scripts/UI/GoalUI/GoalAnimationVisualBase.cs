using BlastGame.BoardItems.Data;
using BlastGame.CommandSystem;
using BlastGame.UI.Animations;
using System;
using UnityEngine;

namespace BlastGame.UI.GoalManagement
{
    public abstract class GoalAnimationVisualBase : MonoBehaviour, ICommand
    {
        protected IUIAnimationEndPoint _animationEndPoint;

        public virtual void Initialize(ItemData data, IUIAnimationEndPoint endPoint)
        {
            _animationEndPoint = endPoint;
        }

        public abstract void Execute(Action OnComplete);
    }
}
