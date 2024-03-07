using BlastGame.ServiceManagement;

namespace BlastGame.AudioManagement
{
    public interface IAudioManager : IService
    {
        public void Play(AudioData data);
        public void PlayWithSuppressDurationAsync(AudioData data, int suppressedMiliseconds);
    }
}
