using BlastGame.ObjectPoolingSystem;
using BlastGame.ParticleManagement.ParticleTypes;
using BlastGame.ServiceManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.ParticleManagement.Core
{
    public class ParticleManager : MonoBehaviour, IParticleManager
    {
        [SerializeField] private BlockParticle _blockParticlePrefab;
        [SerializeField] private GoalParticle _goalParticlePrefab;

        private Dictionary<Type, ParticleBase> _particles = new();

        private IObjectPoolManager _pool;

        private void Awake()
        {
            _pool = ServiceLocator.Instance.GetService<IObjectPoolManager>();

            RegisterParticles();
        }

        private void RegisterParticles()
        {
            _particles.Add(typeof(BlockParticle), _blockParticlePrefab);
            _pool.PrePopulatePool(_blockParticlePrefab, 10);

            _particles.Add(typeof(GoalParticle), _goalParticlePrefab);
            _pool.PrePopulatePool(_goalParticlePrefab, 10);
        }

        public void PlayParticle(Type particleType, Vector3 position, Transform parent = null, object data = null)
        {
            if (_particles.TryGetValue(particleType, out var particle))
            {
                var pooledParticle = _pool.GetObject(particle);

                pooledParticle.transform.position = position;
                pooledParticle.transform.SetParent(parent);

                pooledParticle.Play(data);
            }
        }
    }
}
