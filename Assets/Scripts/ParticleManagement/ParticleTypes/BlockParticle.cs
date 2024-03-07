using BlastGame.BoardItems.Core;
using BlastGame.BoardItems.Data;
using BlastGame.ParticleManagement.Core;
using UnityEngine;

namespace BlastGame.ParticleManagement.ParticleTypes
{
    public class BlockParticle : ParticleBase
    {
        public override void Play(object data)
        {
            ItemData itemData = (ItemData)data;

            Color color = itemData.Color switch
            {
                ItemColor.Yellow => Color.yellow,
                ItemColor.Red => Color.red,
                ItemColor.Blue => Color.cyan,
                ItemColor.Green => Color.green,
                ItemColor.Purple => new Color(143f, 0f, 254f, 1f),
                _ => Color.white,
            };

            var mainModule = _particle.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(color);

            _particle.Play();
        }

        public override void Stop()
        {
            _particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
