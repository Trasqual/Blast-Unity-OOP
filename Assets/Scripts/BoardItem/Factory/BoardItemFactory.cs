using BlastGame.BoardItems.Core;
using BlastGame.BoardItems.Data;
using BlastGame.BoardItems.Strategies.ClickStrategies;
using BlastGame.BoardItems.Strategies.FallStrategies;
using BlastGame.BoardItems.Strategies.NeighbourListenStrategies;
using BlastGame.BoardItems.Strategies.PoppingStrategies;
using BlastGame.BoardItems.Strategies.SettlementStrategies;
using BlastGame.PowerUpSystem;
using BlastGame.ServiceManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlastGame.BoardItems.Factory
{
    [DefaultExecutionOrder(-10)]
    public class BoardItemFactory : MonoBehaviour, IService
    {
        [Header("BoardItem")]
        [SerializeField] private BoardItem _prefab;

        [Header("Item Types")]
        [SerializeField] private List<ItemData> _blockTypes = new();
        [SerializeField] private List<ItemData> _balloonTypes = new();
        [SerializeField] private List<ItemData> _duckTypes = new();

        [Header("Strategies")]
        [SerializeField] private List<ItemClickStrategy> _clickStrategies = new();
        [SerializeField] private List<PoppingStrategy> _poppingStrategies = new();
        [SerializeField] private List<NeighbourListenStrategy> _neighbourListenStrategies = new();
        [SerializeField] private List<ItemSettlementStrategy> _settlementStrategies = new();
        [SerializeField] private List<ItemFallStrategy> _fallStrategies = new();

        [field: Header("PowerUps")]
        [field: SerializeField] public List<PowerUpBase> PowerUps { get; private set; } = new();

        private BoardItemBuilder _itemBuilder;

        private void Awake()
        {
            _itemBuilder = new BoardItemBuilder(_prefab);
        }


        public BoardItem GetBlock(ItemData type)
        {
            return _itemBuilder.WithType(type)
                               .AddFallComponent(null)
                               .AddPopComponent(_poppingStrategies.FirstOrDefault(x => x is BlockPoppingStrategy))
                               .AddClickComponent(_clickStrategies.FirstOrDefault(x => x is MatchFindClickStrategy))
                               .AddMergeComponent()
                               .Build();
        }

        public BoardItem GetBlockWithColor(ItemColor color)
        {
            return GetBlock(_blockTypes.FirstOrDefault(x => x.Color == color));
        }

        public BoardItem GetRandomBlock()
        {
            ItemData randomType = _blockTypes[Random.Range(0, _blockTypes.Count)];
            return GetBlock(randomType);
        }

        public BoardItem GetBalloon(ItemData type)
        {
            return _itemBuilder.WithType(type)
                               .AddFallComponent(null)
                               .AddPopComponent(_poppingStrategies.FirstOrDefault(x => x is BlockPoppingStrategy))
                               .AddNeighbourListenComponent(_neighbourListenStrategies.FirstOrDefault(x => x is PopWithNeighbourListenStrategy))
                               .Build();
        }

        public BoardItem GetBalloonWithColor(ItemColor color)
        {
            return GetBalloon(_balloonTypes.FirstOrDefault(x => x.Color == color));
        }

        public BoardItem GetRandomBalloon()
        {
            ItemData randomType = _balloonTypes[Random.Range(0, _balloonTypes.Count)];
            return GetBalloon(randomType);
        }

        public BoardItem GetDuck()
        {
            return _itemBuilder.WithType(_duckTypes[0])
                               .AddFallComponent(_fallStrategies.FirstOrDefault(x => x is DuckFallStrategy))
                               .AddPopComponent(_poppingStrategies.FirstOrDefault(x => x is DuckPoppingStrategy))
                               .AddSettlementComponent(_settlementStrategies.FirstOrDefault(x => x is PopAtBottomSettlementStrategy))
                               .Build();
        }

        public BoardItem GetRocket()
        {
            var rockets = PowerUps.FindAll(x => x is RocketPowerUp);
            var randomRocketIndex = Random.Range(0, rockets.Count);
            var randomRocket = rockets[randomRocketIndex];
            return _itemBuilder.WithType(null)
                               .AddFallComponent(null)
                               .AddPopComponent(_poppingStrategies.FirstOrDefault(x => x is PowerUpPoppingStrategy))
                               .AddPowerUp(randomRocket)
                               .AddClickComponent(_clickStrategies.FirstOrDefault(x => x is PowerUpClickStrategy))
                               .Build();
        }

        public BoardItem GetBomb()
        {
            var bomb = PowerUps.Find(x => x is BombPowerUp);
            return _itemBuilder.WithType(null)
                               .AddFallComponent(null)
                               .AddPopComponent(_poppingStrategies.FirstOrDefault(x => x is PowerUpPoppingStrategy))
                               .AddClickComponent(_clickStrategies.FirstOrDefault(x => x is PowerUpClickStrategy))
                               .AddPowerUp(bomb)
                               .Build();
        }
    }
}
