using BlastGame.ObjectPoolingSystem;
using BlastGame.ServiceManagement;
using UnityEngine;

namespace BlastGame.ParticleManagement.Core
{
    public abstract class ParticleBase : MonoBehaviour, IParticle
    {
        [SerializeField] protected ParticleSystem _particle;

        public abstract void Play(object data);

        public abstract void Stop();

        private void OnParticleSystemStopped()
        {
            ServiceLocator.Instance.GetService<IObjectPoolManager>().ReleaseObject(this);
        }
    }
}
