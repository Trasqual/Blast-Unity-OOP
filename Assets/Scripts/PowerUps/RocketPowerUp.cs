using BlastGame.BoardItems.Core;
using BlastGame.BoardItems.Factory;
using BlastGame.BoardManagement.Tiles;
using BlastGame.EventSystem;
using BlastGame.ObjectPoolingSystem;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.PowerUpSystem
{
    public abstract class RocketPowerUp : PowerUpBase
    {
        [SerializeField] private RocketMovingPart _rocket1;
        [SerializeField] private RocketMovingPart _rocket2;

        private Vector3 _rocket1Pos;
        private Vector3 _rocket2Pos;

        private int completeCount = 0;

        private void Awake()
        {
            _rocket1Pos = _rocket1.transform.localPosition;
            _rocket2Pos = _rocket2.transform.localPosition;

            _rocket1.OnMovementCompleted += OnComplete;
            _rocket2.OnMovementCompleted += OnComplete;
        }

        public override void Activate(Tile startTile)
        {
            completeCount = 0;

            _rocket1.Activate(startTile);
            _rocket2.Activate(startTile);

            EventManager.Instance.TriggerEvent<BlockGameLoopEvent>();
        }

        private void OnComplete()
        {
            completeCount++;
            if (completeCount == 2)
            {
                _rocket1.transform.localPosition = _rocket1Pos;
                _rocket2.transform.localPosition = _rocket2Pos;

                ServiceLocator.Instance.GetService<IObjectPoolManager>().ReleaseObject(this);

                EventManager.Instance.TriggerEvent<ReleaseGameLoopEvent>();
            }
        }

        public override BoardItem Build()
        {
            return ServiceLocator.Instance.GetService<BoardItemFactory>().GetRocket();
        }
    }
}
