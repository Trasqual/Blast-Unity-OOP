using BlastGame.BoardManagement;
using BlastGame.EventSystem;
using BlastGame.GoalSystem;
using BlastGame.MoveManagement;

namespace BlastGame.GameLoopManagement
{
    public class GameLoopManager : IGameLoopManager
    {
        private IGoalManager _goalManager;

        private IMoveManager _moveManager;

        private BoardBase _board;

        private bool _isBlocked;

        public GameLoopManager(IGoalManager goalManager, IMoveManager moveManager, BoardBase board)
        {
            _goalManager = goalManager;
            _moveManager = moveManager;
            _board = board;

            EventManager.Instance.AddListener<ItemCollectedEvent>(OnItemCollected);
            EventManager.Instance.AddListener<BlockGameLoopEvent>(OnGameLoopBlocked);
            EventManager.Instance.AddListener<ReleaseGameLoopEvent>(OnGameLoopReleased);
        }

        private void OnItemCollected(object eventData)
        {
            EventManager.Instance.TriggerEvent<GameLoopHasStartedEvent>();

            ItemCollectionData data = ((ItemCollectedEvent)eventData).ItemCollectionData;

            if (_goalManager.CanProcessItem(data))
            {
                _goalManager.RegisterGoalItem(data);

                if (_goalManager.AllGoalsAreCompleted())
                {
                    EventManager.Instance.TriggerEvent<AllGoalsAreCompletedEvent>();
                }
                else
                {
                    CheckMovesLeft();
                }
            }
            else
            {
                CheckMovesLeft();
            }
        }

        private void CheckMovesLeft()
        {
            if (_moveManager.NoMovesLeft())
            {
                if (_isBlocked)
                {
                    return;
                }
                EventManager.Instance.TriggerEvent<OutOfMovesEvent>();
            }
            else
            {
                EventManager.Instance.TriggerEvent<GameLoopHasEndedEvent>();
            }
        }

        private void OnGameLoopBlocked(object eventData)
        {
            _isBlocked = true;
        }

        private void OnGameLoopReleased(object eventData)
        {
            _isBlocked = false;
            CheckIfBoardIsSettledWithNoMovesLeft();
        }

        private void CheckIfBoardIsSettledWithNoMovesLeft()
        {
            for (int i = 0; i < _board.Size.x; i++)
            {
                for (int j = 0; j < _board.Size.y; j++)
                {
                    if (_board.GetTile(i, j).State is not TileSettledState)
                    {
                        return;
                    }
                }
            }

            CheckMovesLeft();
        }

        public void Dispose()
        {
            EventManager.Instance.RemoveListener<ItemCollectedEvent>(OnItemCollected);
            EventManager.Instance.RemoveListener<BlockGameLoopEvent>(OnGameLoopBlocked);
            EventManager.Instance.RemoveListener<ReleaseGameLoopEvent>(OnGameLoopReleased);
        }
    }
}
