using BlastGame.ServiceManagement;
using System;

namespace BlastGame.MoveManagement
{
    public interface IMoveManager : IService, IDisposable
    {
        public void Initialize(int moves);
        public void ProcessMove(int processedMoves);
        public bool NoMovesLeft();
    }
}
