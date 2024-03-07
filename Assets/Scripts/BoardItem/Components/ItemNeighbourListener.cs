using BlastGame.BoardItems.Data;
using BlastGame.BoardItems.Strategies.NeighbourListenStrategies;
using BlastGame.BoardManagement.Tiles;
using BlastGame.MatchManagement;

namespace BlastGame.BoardItems.Components
{
    public class ItemNeighbourListener : BoardItemComponentBase
    {
        NeighbourListenStrategy _neighbourListenStrategy;

        Tile _previousTile;

        protected override void Awake()
        {
            base.Awake();

            _attachedItem.OnItemSettled += OnItemSettled;
        }

        public void SetStrategy(NeighbourListenStrategy neighbourListenStrategy)
        {
            _neighbourListenStrategy = neighbourListenStrategy;
        }

        public void ExecuteStrategy(object neighbourPoppedBy, ItemData data)
        {
            if (neighbourPoppedBy is IMatchManager)
            {
                _neighbourListenStrategy.ExecuteStrategy(_attachedItem);
            }
        }

        private void OnItemSettled()
        {
            ClearPreviousTile();

            _previousTile = _attachedItem.CurrentTile;

            foreach (var neighbour in _attachedItem.CurrentTile.Neighbours.Values)
            {
                neighbour.OnPop += ExecuteStrategy;
                neighbour.OnMerge += ExecuteStrategy;
            }
        }

        private void ClearPreviousTile()
        {
            if (_previousTile != null)
            {
                foreach (var previousNeighbour in _previousTile.Neighbours.Values)
                {
                    previousNeighbour.OnPop -= ExecuteStrategy;
                    previousNeighbour.OnMerge -= ExecuteStrategy;
                }
            }
        }

        private void OnDestroy()
        {
            ClearPreviousTile();
            _previousTile = null;
            _attachedItem.OnItemSettled -= OnItemSettled;
        }
    }
}
