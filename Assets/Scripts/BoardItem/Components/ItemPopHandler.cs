using BlastGame.BoardItems.Strategies.PoppingStrategies;

namespace BlastGame.BoardItems.Components
{
    public class ItemPopHandler : BoardItemComponentBase
    {
        private PoppingStrategy _strategy;

        public void SetStrategy(PoppingStrategy strategy)
        {
            _strategy = strategy;
        }

        public bool CanBePoppedBy(object poppedBy)
        {
            return _strategy.CanBePopped(poppedBy);
        }

        public void Pop()
        {
            _attachedItem.ReleaseTile();
            _strategy.ExecuteStrategy(_attachedItem);
        }
    }
}
