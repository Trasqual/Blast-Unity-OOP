namespace BlastGame.ParticleManagement.Core
{
    public interface IParticle
    {
        public void Play(object data);
        public void Stop();
    }
}
