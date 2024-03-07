using System;

namespace BlastGame.UI.Animations
{
    public interface IAnimatableUIItem
    {
        public void AddListener(Action listener);
    }
}
