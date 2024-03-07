using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BlastGame.AudioManagement
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        private AudioSource _audioSource;

        private const string audioPath = "Audio/";

        private List<AudioType> _playingAudioTypes = new List<AudioType>();

        private bool _isPlaying;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioData data)
        {
            _audioSource.pitch = data.Pitch;
            _audioSource.volume = data.Volume;
            _audioSource.loop = data.Looping;

            _audioSource.PlayOneShot(GetAudioClip(data.AudioType));
        }

        public async void PlayWithSuppressDurationAsync(AudioData data, int suppressedMiliseconds)
        {
            if (_isPlaying)
            {
                return;
            }

            _isPlaying = true;

            AudioClip clip = GetAudioClip(data.AudioType);

            int delayDuration = clip.length * 100f < suppressedMiliseconds ? Mathf.RoundToInt(clip.length * 100f) : suppressedMiliseconds;

            Play(data);

            await Task.Delay(delayDuration);

            _isPlaying = false;
        }

        private AudioClip GetAudioClip(AudioType audioType)
        {
            string path = audioPath;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(path);
            stringBuilder.Append(audioType.ToString());

            path = stringBuilder.ToString();

            return Resources.Load<AudioClip>(path);
        }
    }
}
