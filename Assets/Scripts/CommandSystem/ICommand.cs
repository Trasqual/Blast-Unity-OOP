using System;

namespace BlastGame.CommandSystem
{
    public interface ICommand
    {
        public void Execute(Action OnComplete);
    }
}
