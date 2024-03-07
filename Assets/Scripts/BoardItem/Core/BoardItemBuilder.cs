using BlastGame.BoardItems.Components;
using BlastGame.BoardItems.Data;
using BlastGame.BoardItems.Strategies.ClickStrategies;
using BlastGame.BoardItems.Strategies.FallStrategies;
using BlastGame.BoardItems.Strategies.NeighbourListenStrategies;
using BlastGame.BoardItems.Strategies.PoppingStrategies;
using BlastGame.BoardItems.Strategies.SettlementStrategies;
using BlastGame.ObjectPoolingSystem;
using BlastGame.PowerUpSystem;
using BlastGame.ServiceManagement;
using Unity.VisualScripting;
using UnityEngine;

namespace BlastGame.BoardItems.Core
{
    public class BoardItemBuilder
    {
        private BoardItem _prefab;
        private BoardItem _createdItem;

        public BoardItemBuilder(BoardItem prefab)
        {
            _prefab = prefab;
            _createdItem = ServiceLocator.Instance.GetService<IObjectPoolManager>().GetObject(_prefab);
        }

        public BoardItemBuilder WithType(ItemData type)
        {
            _createdItem.gameObject.SetActive(true);
            _createdItem.SetData(type);
            return this;
        }

        public BoardItemBuilder AddFallComponent(ItemFallStrategy itemFallStrategy)
        {
            _createdItem.AddComponent<ItemFallHandler>().SetStrategy(itemFallStrategy);
            return this;
        }

        public BoardItemBuilder AddClickComponent(ItemClickStrategy clickStrategy)
        {
            _createdItem.AddComponent<ItemClickHandler>().SetStrategy(clickStrategy);
            return this;
        }

        public BoardItemBuilder AddPopComponent(PoppingStrategy poppingStrategy)
        {
            _createdItem.AddComponent<ItemPopHandler>().SetStrategy(poppingStrategy);
            return this;
        }

        public BoardItemBuilder AddNeighbourListenComponent(NeighbourListenStrategy neighbourListenStrategy)
        {
            _createdItem.AddComponent<ItemNeighbourListener>().SetStrategy(neighbourListenStrategy);
            return this;
        }

        public BoardItemBuilder AddSettlementComponent(ItemSettlementStrategy settlementStrategy)
        {
            _createdItem.AddComponent<ItemSettlementHandler>().SetStrategy(settlementStrategy);
            return this;
        }

        public BoardItemBuilder AddMergeComponent()
        {
            _createdItem.AddComponent<ItemMergeHandler>();
            return this;
        }

        public BoardItemBuilder AddPowerUp(PowerUpBase powerUpPrefab)
        {
            PowerUpBase powerUp = ServiceLocator.Instance.GetService<IObjectPoolManager>().GetObject(powerUpPrefab);
            powerUp.transform.SetParent(_createdItem.transform);
            powerUp.transform.localPosition = Vector3.zero;
            return this;
        }

        public BoardItem Build()
        {
            BoardItem builtItem = _createdItem;
            _createdItem = ServiceLocator.Instance.GetService<IObjectPoolManager>().GetObject(_prefab);
            _createdItem.gameObject.SetActive(false);

            return builtItem;
        }
    }
}
