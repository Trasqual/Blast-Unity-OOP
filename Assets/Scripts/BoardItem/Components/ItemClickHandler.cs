using BlastGame.BoardItems.Strategies.ClickStrategies;

namespace BlastGame.BoardItems.Components
{
    public class ItemClickHandler : BoardItemComponentBase
    {
        private ItemClickStrategy _strategy;

        public void SetStrategy(ItemClickStrategy strategy)
        {
            _strategy = strategy;
        }

        public void ExecuteClick()
        {
            _strategy.Execute(_attachedItem);
        }
    }
}
