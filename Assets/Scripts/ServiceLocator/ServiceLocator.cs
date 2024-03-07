using BlastGame.AudioManagement;
using BlastGame.BoardItems.Factory;
using BlastGame.BoardManagement;
using BlastGame.FillsAndFallsSystem;
using BlastGame.GameLoopManagement;
using BlastGame.GoalSystem;
using BlastGame.InputManagement;
using BlastGame.LevelManagement;
using BlastGame.MatchFindingSystem;
using BlastGame.MatchManagement;
using BlastGame.MoveManagement;
using BlastGame.ObjectPoolingSystem;
using BlastGame.ParticleManagement.Core;
using BlastGame.PowerUpSystem;
using BlastGame.UI.Animations;
using BlastGame.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.ServiceManagement
{
    [DefaultExecutionOrder(-100)]
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        [SerializeField] private BoardBase _board;

        [SerializeField] private InputBase _input;

        [SerializeField] private BoardItemFactory _itemFactory;

        [SerializeField] private FallMovementController _fallMovementController;

        [SerializeField] private ObjectPoolManager _objectPoolManager;

        [SerializeField] private LevelManager _levelManager;

        [SerializeField] private ParticleManager _particleManager;

        [SerializeField] private AudioManager _audioManager;

        public Camera BoardCamera { get; private set; }

        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();


        protected override void Awake()
        {
            base.Awake();

            PreRegisterServices();

            BoardCamera = Camera.main;
        }

        private void PreRegisterServices()
        {
            RegisterServiceByType(typeof(IObjectPoolManager), _objectPoolManager);

            RegisterServiceByType(typeof(ILevelManager), _levelManager);

            RegisterService(_itemFactory);

            RegisterServiceByType(typeof(BoardBase), _board);

            RegisterService(_input);

            RegisterService(_fallMovementController);

            RegisterServiceByType(typeof(IParticleManager), _particleManager);

            RegisterServiceByType(typeof(IAudioManager), _audioManager);

            RegisterServiceByType(typeof(IMatchFinder), new FlowFieldMatchFinder(_board));

            RegisterServiceByType(typeof(IPowerUpSelector), new CountDependentPowerUpSelector());

            RegisterServiceByType(typeof(IMatchManager), new PowerUpOrPopMatchManager(GetService<IPowerUpSelector>()));

            RegisterServiceByType(typeof(IGoalManager), new GoalManager(_levelManager.GetLevelData()));

            RegisterService(new UIAnimationCommandManager());

            RegisterServiceByType(typeof(IMoveManager), new MoveManager(_levelManager.GetLevelData().GetMoves()));

            RegisterServiceByType(typeof(IGameLoopManager), new GameLoopManager(GetService<IGoalManager>(), GetService<IMoveManager>(), GetService<BoardBase>()));

            RegisterServiceByType(typeof(IGoalProvider), _levelManager.GetLevelData());

            RegisterServiceByType(typeof(IMoveProvider), _levelManager.GetLevelData());
        }

        public void RegisterService(IService service)
        {
            _services.TryAdd(service.GetType(), service);
        }

        public void RegisterServiceByType(Type serviceType, IService service)
        {
            _services.TryAdd(serviceType, service);
        }

        public T GetService<T>() where T : IService
        {
            _services.TryGetValue(typeof(T), out var service);
            return (T)service;
        }

        private void OnDestroy()
        {
            foreach (var service in _services.Values)
            {
                if (service is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
