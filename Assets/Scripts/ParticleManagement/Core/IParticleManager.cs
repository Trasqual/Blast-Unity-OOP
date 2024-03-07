using BlastGame.ServiceManagement;
using System;
using UnityEngine;

namespace BlastGame.ParticleManagement.Core
{
    public interface IParticleManager : IService
    {
        public void PlayParticle(Type particleType, Vector3 position, Transform parent = null, object data = null);
    }
}
