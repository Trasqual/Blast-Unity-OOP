using BlastGame.EventSystem;

namespace BlastGame.MoveManagement
{
    public class MoveManager : IMoveManager
    {
        private int _moves;

        public MoveManager(int moves)
        {
            Initialize(moves);

            SubscribeToEvents();
        }

        public void Initialize(int moves)
        {
            _moves = moves;
        }

        private void SubscribeToEvents()
        {
            EventManager.Instance.AddListener<LegalMoveMadeEvent>(OnMoveMade);
        }

        private void OnMoveMade(object eventData)
        {
            ProcessMove(-1);
        }

        public void ProcessMove(int processedMoves)
        {
            _moves += processedMoves;

            if (_moves <= 0)
            {
                _moves = 0;
            }

            EventManager.Instance.TriggerEvent<MoveProcessedEvent>(new MoveProcessedEvent { ProcessedMoves = processedMoves });
        }

        public bool NoMovesLeft()
        {
            return _moves <= 0;
        }

        private void UnsubscribeToEvents()
        {
            EventManager.Instance.RemoveListener<LegalMoveMadeEvent>(OnMoveMade);
        }

        public void Dispose()
        {
            UnsubscribeToEvents();
        }
    }
}
