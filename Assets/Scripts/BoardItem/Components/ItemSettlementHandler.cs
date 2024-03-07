using BlastGame.BoardItems.Strategies.SettlementStrategies;

namespace BlastGame.BoardItems.Components
{
    public class ItemSettlementHandler : BoardItemComponentBase
    {
        ItemSettlementStrategy _itemSettlementStrategy;

        protected override void Awake()
        {
            base.Awake();

            _attachedItem.OnItemSettled += OnItemSettled;
        }

        public void SetStrategy(ItemSettlementStrategy itemSettlementStrategy)
        {
            _itemSettlementStrategy = itemSettlementStrategy;
        }

        private void OnItemSettled()
        {
            _itemSettlementStrategy.ExecuteStrategy(_attachedItem);
        }

        private void OnDestroy()
        {
            _attachedItem.OnItemSettled -= OnItemSettled;
        }
    }
}
