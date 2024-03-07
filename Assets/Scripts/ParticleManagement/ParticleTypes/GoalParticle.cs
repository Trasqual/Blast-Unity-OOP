using BlastGame.ParticleManagement.Core;
using UnityEngine;

namespace BlastGame.ParticleManagement.ParticleTypes
{
    public class GoalParticle : ParticleBase
    {
        public override void Play(object data)
        {
            _particle.Play();
        }

        public override void Stop()
        {
            _particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
