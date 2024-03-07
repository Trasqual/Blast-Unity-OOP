using BlastGame.ServiceManagement;

namespace BlastGame.MoveManagement
{
    public interface IMoveProvider: IService
    {
        public int GetMoves();
    }
}
